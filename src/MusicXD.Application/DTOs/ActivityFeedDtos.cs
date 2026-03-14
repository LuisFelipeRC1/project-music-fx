namespace MusicXD.Application.DTOs;

public record ActivityFeedResponse(
    Guid ActivityId,
    Guid UserId,
    string Username,
    string ActivityType,
    Guid TargetId,
    DateTime CreatedAt);
