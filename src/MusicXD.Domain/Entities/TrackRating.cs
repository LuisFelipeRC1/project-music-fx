namespace MusicXD.Domain.Entities;

public class TrackRating
{
    public Guid TrackId { get; private set; }
    public Guid UserId { get; private set; }
    public int Rating { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Track Track { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private TrackRating() { }

    public static TrackRating Create(Guid trackId, Guid userId, int rating)
    {
        if (rating < 1 || rating > 10)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 10.");

        return new TrackRating
        {
            TrackId = trackId,
            UserId = userId,
            Rating = rating,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
