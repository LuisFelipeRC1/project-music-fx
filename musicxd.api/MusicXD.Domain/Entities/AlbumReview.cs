using MusicXD.Domain.Abstractions;
using MusicXD.Domain.Events;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Entities;

public class AlbumReview : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid AlbumId { get; private set; }
    public Rating Rating { get; private set; } = null!;
    public ReviewText ReviewText { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public AlbumReview(Guid userId, Guid albumId, Rating rating, ReviewText reviewText)
    {
        Id = Guid.NewGuid();
        UserId = ValidateForeignKey(userId, nameof(userId));
        AlbumId = ValidateForeignKey(albumId, nameof(albumId));
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        ReviewText = reviewText ?? throw new ArgumentNullException(nameof(reviewText));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        RaiseDomainEvent(new AlbumReviewed(Id, UserId, AlbumId, Rating.Value, CreatedAt));
    }

    public void UpdateReview(Rating rating, ReviewText reviewText)
    {
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        ReviewText = reviewText ?? throw new ArgumentNullException(nameof(reviewText));
        UpdatedAt = DateTime.UtcNow;
    }

    private static Guid ValidateForeignKey(Guid value, string paramName)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);

        return value;
    }

    private AlbumReview() { }
}
