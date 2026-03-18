using MusicXD.Domain.Entities;
using MusicXD.Domain.Events;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.Entities;

public class AlbumReviewTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesAndRaiseAlbumReviewedEvent()
    {
        var review = new AlbumReview(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Rating(4.5m),
            new ReviewText("Excellent album."));

        Assert.Equal(4.5m, review.Rating.Value);
        Assert.Equal("Excellent album.", review.ReviewText.Value);
        Assert.Single(review.DomainEvents);

        var domainEvent = Assert.IsType<AlbumReviewed>(review.DomainEvents.Single());
        Assert.Equal(review.Id, domainEvent.ReviewId);
        Assert.Equal(review.UserId, domainEvent.UserId);
        Assert.Equal(review.AlbumId, domainEvent.AlbumId);
        Assert.Equal(4.5m, domainEvent.Rating);
    }

    [Fact]
    public void UpdateReview_ShouldReplaceRatingAndText()
    {
        var review = new AlbumReview(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new Rating(4.0m),
            new ReviewText("Good album."));

        review.UpdateReview(new Rating(5.0m), new ReviewText("Perfect album."));

        Assert.Equal(5.0m, review.Rating.Value);
        Assert.Equal("Perfect album.", review.ReviewText.Value);
        Assert.True(review.UpdatedAt >= review.CreatedAt);
    }
}
