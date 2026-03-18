using MusicXD.Domain.Abstractions;
using MusicXD.Domain.Events;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class TrackRating : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid TrackId { get; private set; }
    public Rating Rating { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public TrackRating(Guid userId, Guid trackId, Rating rating)
    {
        Id = Guid.NewGuid();
        UserId = ValidateForeignKey(userId, nameof(userId));
        TrackId = ValidateForeignKey(trackId, nameof(trackId));
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        RaiseDomainEvent(new TrackRated(Id, UserId, TrackId, Rating.Value, CreatedAt));
    }

    public void UpdateRating(Rating rating)
    {
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        UpdatedAt = DateTime.UtcNow;
    }

    private static Guid ValidateForeignKey(Guid value, string paramName)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);

        return value;
    }

    private TrackRating() { }
}
