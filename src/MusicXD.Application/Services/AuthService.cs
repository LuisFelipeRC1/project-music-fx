using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Interfaces;

namespace MusicXD.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var existingByEmail = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingByEmail is not null)
            throw new InvalidOperationException("Email is already in use.");

        var existingByUsername = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (existingByUsername is not null)
            throw new InvalidOperationException("Username is already taken.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = User.Create(request.Username, request.Email, passwordHash);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _tokenService.GenerateToken(user.UserId, user.Username, user.Email);
        return new AuthResponse(token, user.Username, user.UserId);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = _tokenService.GenerateToken(user.UserId, user.Username, user.Email);
        return new AuthResponse(token, user.Username, user.UserId);
    }
}
