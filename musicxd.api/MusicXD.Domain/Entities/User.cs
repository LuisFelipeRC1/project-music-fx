using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public string? Bio { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public string? SpotifyUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public User(
        string username,
        Email email,
        string passwordHash,
        string? bio = null,
        string? profileImageUrl = null,
        string? spotifyUserId = null)
    {
        Id = Guid.NewGuid();
        Username = ValidateRequired(username, nameof(username), 100);
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = ValidateRequired(passwordHash, nameof(passwordHash));
        Bio = NormalizeOptional(bio, 1000, nameof(bio));
        ProfileImageUrl = NormalizeOptional(profileImageUrl, 2048, nameof(profileImageUrl));
        SpotifyUserId = NormalizeOptional(spotifyUserId, 200, nameof(spotifyUserId));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void UpdateProfile(string? bio, string? profileImageUrl)
    {
        Bio = NormalizeOptional(bio, 1000, nameof(bio));
        ProfileImageUrl = NormalizeOptional(profileImageUrl, 2048, nameof(profileImageUrl));
        Touch();
    }

    public void LinkSpotifyAccount(string spotifyUserId)
    {
        SpotifyUserId = ValidateRequired(spotifyUserId, nameof(spotifyUserId), 200);
        Touch();
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = ValidateRequired(passwordHash, nameof(passwordHash));
        Touch();
    }

    private void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    private static string ValidateRequired(string value, string paramName, int? maxLength = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);

        var normalized = value.Trim();

        if (maxLength.HasValue && normalized.Length > maxLength.Value)
            throw new ArgumentException($"{paramName} cannot be longer than {maxLength.Value} characters.", paramName);

        return normalized;
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
