using MusicXD.Domain.Abstractions;
using MusicXD.Domain.Events;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class User : Entity
{
    public Guid Id { get; private set; }
    public Username Username { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PasswordHash PasswordHash { get; private set; } = null!;
    public string? Bio { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public List<Follow> Followers { get; private set; } = new();
    public List<Follow> Following { get; private set; } = new();
    public List<AlbumReview> AlbumReviews { get; private set; } = new();
    public List<TrackRating> TrackRatings { get; private set; } = new();

    public User(
        Username username,
        Email email,
        PasswordHash passwordHash,
        string? bio = null,
        string? profileImageUrl = null)
    {
        Id = Guid.NewGuid();
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Bio = NormalizeOptional(bio, 1000, nameof(bio));
        ProfileImageUrl = NormalizeOptional(profileImageUrl, 2048, nameof(profileImageUrl));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        RaiseDomainEvent(new UserRegistered(Id, CreatedAt));
    }

    public void UpdateProfile(string? bio, string? profileImageUrl)
    {
        Bio = NormalizeOptional(bio, 1000, nameof(bio));
        ProfileImageUrl = NormalizeOptional(profileImageUrl, 2048, nameof(profileImageUrl));
        Touch();
    }

    public void ChangePassword(PasswordHash passwordHash)
    {
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Touch();
    }

    private void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    private static string? NormalizeOptional(string? value, int maxLength, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var normalized = value.Trim();

        if (normalized.Length > maxLength)
            throw new ArgumentException($"{paramName} cannot be longer than {maxLength} characters.", paramName);

        return normalized;
    }

    private User() { }
}
