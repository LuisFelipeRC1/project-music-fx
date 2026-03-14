using MusicXD.Domain.Enums;

namespace MusicXD.Domain.Entities;

public class ActivityFeed
{
    public Guid ActivityId { get; private set; }
    public Guid UserId { get; private set; }
    public ActivityType ActivityType { get; private set; }
    public Guid TargetId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User User { get; private set; } = null!;

    private ActivityFeed() { }

    public static ActivityFeed Create(Guid userId, ActivityType activityType, Guid targetId)
    {
        return new ActivityFeed
        {
            ActivityId = Guid.NewGuid(),
            UserId = userId,
            ActivityType = activityType,
            TargetId = targetId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
