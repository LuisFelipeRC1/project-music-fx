namespace MusicXD.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string? Bio { get; private set; }
    public string? AvatarUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<Review> Reviews { get; private set; } = new List<Review>();
    public ICollection<TrackRating> TrackRatings { get; private set; } = new List<TrackRating>();
    public ICollection<Follow> Followers { get; private set; } = new List<Follow>();
    public ICollection<Follow> Following { get; private set; } = new List<Follow>();
    public ICollection<ActivityFeed> Activities { get; private set; } = new List<ActivityFeed>();

    private User() { }

    public static User Create(string username, string email, string passwordHash)
    {
        return new User
        {
            UserId = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateProfile(string? bio, string? avatarUrl)
    {
        Bio = bio;
        AvatarUrl = avatarUrl;
        UpdatedAt = DateTime.UtcNow;
    }
}
