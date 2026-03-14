namespace MusicXD.Domain.Entities;

public class Follow
{
    public Guid FollowerId { get; private set; }
    public Guid FollowingId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User Follower { get; private set; } = null!;
    public User Following { get; private set; } = null!;

    private Follow() { }

    public static Follow Create(Guid followerId, Guid followingId)
    {
        if (followerId == followingId)
            throw new InvalidOperationException("A user cannot follow themselves.");

        return new Follow
        {
            FollowerId = followerId,
            FollowingId = followingId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
