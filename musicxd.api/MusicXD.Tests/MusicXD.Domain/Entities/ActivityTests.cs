using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;

namespace MusicXD.Domain.Tests.Entities;

public class ActivityTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var userId = Guid.NewGuid();

        var activity = new Activity(userId, ActivityType.AlbumReviewed, "  payload  ");

        Assert.Equal(userId, activity.UserId);
        Assert.Equal(ActivityType.AlbumReviewed, activity.Type);
        Assert.Equal("payload", activity.Payload);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenPayloadIsEmpty()
    {
        var action = () => new Activity(Guid.NewGuid(), ActivityType.TrackRated, "   ");

        Assert.Throws<ArgumentException>(action);
    }
}
