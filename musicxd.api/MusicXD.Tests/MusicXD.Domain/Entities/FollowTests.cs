using MusicXD.Domain.Entities;
using MusicXD.Domain.Events;

namespace MusicXD.Domain.Tests.Entities;

public class FollowTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesAndRaiseUserFollowedEvent()
    {
        var followerId = Guid.NewGuid();
        var followingId = Guid.NewGuid();

        var follow = new Follow(followerId, followingId);

        Assert.Equal(followerId, follow.FollowerId);
        Assert.Equal(followingId, follow.FollowingId);
        Assert.Single(follow.DomainEvents);

        var domainEvent = Assert.IsType<UserFollowed>(follow.DomainEvents.Single());
        Assert.Equal(follow.Id, domainEvent.FollowId);
        Assert.Equal(followerId, domainEvent.FollowerId);
        Assert.Equal(followingId, domainEvent.FollowingId);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenUserFollowsThemselves()
    {
        var userId = Guid.NewGuid();

        var action = () => new Follow(userId, userId);

        Assert.Throws<ArgumentException>(action);
    }
}
