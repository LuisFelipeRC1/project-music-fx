using MediatR;
using Microsoft.EntityFrameworkCore;
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
        Username username;
        Email email;

        try
        {
            username = new Username(request.Username);
            email = new Email(request.Email);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message, nameof(request), ex);
        }

        if (await _context.Users.AnyAsync(u => u.Username == username, cancellationToken))
            throw new ArgumentException("Username is already in use.");

        if (await _context.Users.AnyAsync(u => u.Email == email, cancellationToken))
            throw new ArgumentException("Email is already registered.");

        var user = new User(
            username,
            email,
            new PasswordHash(BCrypt.Net.BCrypt.HashPassword(request.Password)));

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username.Value,
            Email = user.Email.Value,
            CreatedAt = user.CreatedAt
        };
    }
}
