using MusicXD.Domain.Enums;

namespace MusicXD.Domain.Entities;

public class ActivityFeed
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public ActivityType ActivityType { get; private set; }
    public string Payload { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public ActivityFeed(Guid userId, ActivityType activityType, string payload)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("userId cannot be empty.", nameof(userId));

        Id = Guid.NewGuid();
        UserId = userId;
        ActivityType = activityType;
        Payload = ValidatePayload(payload);
        CreatedAt = DateTime.UtcNow;
    }

    private static string ValidatePayload(string payload)
    {
        if (string.IsNullOrWhiteSpace(payload))
            throw new ArgumentException("payload cannot be empty.", nameof(payload));

        return payload.Trim();
    }

    private ActivityFeed() { }
}
