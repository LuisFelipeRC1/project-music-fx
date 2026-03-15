using System.Text.RegularExpressions;
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

    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex UsernameRegex = new(
        @"^[a-zA-Z0-9_]{3,50}$",
        RegexOptions.Compiled);

    public RegisterUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Username, email and password are required.");

        if (!EmailRegex.IsMatch(request.Email))
            throw new ArgumentException("Invalid email format.");

        if (!UsernameRegex.IsMatch(request.Username))
            throw new ArgumentException("Username must be 3–50 characters and contain only letters, numbers, or underscores.");

        var normalizedEmail = request.Email.ToLowerInvariant();
        var normalizedUsername = request.Username.ToLowerInvariant();

        if (await _context.Users.AnyAsync(u => u.Username.ToLower() == normalizedUsername, cancellationToken))
            throw new ArgumentException("Username is already in use.");

        if (await _context.Users.AnyAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken))
            throw new ArgumentException("Email is already registered.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = normalizedEmail,
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
            Email = user.Email.Value,
            CreatedAt = user.CreatedAt
        };
    }
}
