namespace MusicXD.Domain.Entities;

public class Review
{
    public Guid ReviewId { get; private set; }
    public Guid AlbumId { get; private set; }
    public Guid UserId { get; private set; }
    public int Rating { get; private set; }
    public string? ReviewText { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Album Album { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private Review() { }

    public static Review Create(Guid albumId, Guid userId, int rating, string? reviewText = null)
    {
        if (rating < 1 || rating > 10)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 10.");

        return new Review
        {
            ReviewId = Guid.NewGuid(),
            AlbumId = albumId,
            UserId = userId,
            Rating = rating,
            ReviewText = reviewText,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
