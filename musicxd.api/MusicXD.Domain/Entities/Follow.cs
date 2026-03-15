namespace MusicXD.Domain.Entities;

public class Follow
{
    public Guid Id { get; private set; }
    public Guid FollowerId { get; private set; }
    public Guid FolloweeId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Follow(Guid followerId, Guid followeeId)
    {
        if (followerId == Guid.Empty)
            throw new ArgumentException("followerId cannot be empty.", nameof(followerId));

        if (followeeId == Guid.Empty)
            throw new ArgumentException("followeeId cannot be empty.", nameof(followeeId));

        if (followerId == followeeId)
            throw new ArgumentException("A user cannot follow themselves.", nameof(followeeId));

        Id = Guid.NewGuid();
        FollowerId = followerId;
        FolloweeId = followeeId;
        CreatedAt = DateTime.UtcNow;
    }

    private Follow() { }
}
