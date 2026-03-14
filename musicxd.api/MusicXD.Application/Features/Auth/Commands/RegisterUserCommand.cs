using MediatR;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;

namespace MusicXD.Application.Features.Auth.Commands;

public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<UserDto>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}
