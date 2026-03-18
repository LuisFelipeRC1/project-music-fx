using MusicXD.Domain.Abstractions;
using MusicXD.Domain.Events;

namespace MusicXD.Domain.Entities;

public class Follow : Entity
{
    public Guid Id { get; private set; }
    public Guid FollowerId { get; private set; }
    public Guid FollowingId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Follow(Guid followerId, Guid followingId)
    {
        if (followerId == Guid.Empty)
            throw new ArgumentException("followerId cannot be empty.", nameof(followerId));

        if (followingId == Guid.Empty)
            throw new ArgumentException("followingId cannot be empty.", nameof(followingId));

        if (followerId == followingId)
            throw new ArgumentException("A user cannot follow themselves.", nameof(followingId));

        Id = Guid.NewGuid();
        FollowerId = followerId;
        FollowingId = followingId;
        CreatedAt = DateTime.UtcNow;
        RaiseDomainEvent(new UserFollowed(Id, FollowerId, FollowingId, CreatedAt));
    }

    private Follow() { }
}
