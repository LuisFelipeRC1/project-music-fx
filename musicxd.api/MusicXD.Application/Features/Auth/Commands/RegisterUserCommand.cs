using MediatR;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;

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
        var user = new User(
            request.Username,
            new Email(request.Email),
            BCrypt.Net.BCrypt.HashPassword(request.Password));

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email.Value,
            CreatedAt = user.CreatedAt
        };
    }
}
