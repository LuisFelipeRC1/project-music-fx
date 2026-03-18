using MusicXD.Domain.Entities;
using MusicXD.Domain.Events;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.Entities;

public class TrackRatingTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesAndRaiseTrackRatedEvent()
    {
        var rating = new TrackRating(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Rating(3.5m));

        Assert.Equal(3.5m, rating.Rating.Value);
        Assert.Single(rating.DomainEvents);

        var domainEvent = Assert.IsType<TrackRated>(rating.DomainEvents.Single());
        Assert.Equal(rating.Id, domainEvent.RatingId);
        Assert.Equal(rating.UserId, domainEvent.UserId);
        Assert.Equal(rating.TrackId, domainEvent.TrackId);
        Assert.Equal(3.5m, domainEvent.Rating);
    }

    [Fact]
    public void UpdateRating_ShouldReplaceRating()
    {
        var rating = new TrackRating(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Rating(2.0m));

        rating.UpdateRating(new Rating(4.0m));

        Assert.Equal(4.0m, rating.Rating.Value);
        Assert.True(rating.UpdatedAt >= rating.CreatedAt);
    }
}
