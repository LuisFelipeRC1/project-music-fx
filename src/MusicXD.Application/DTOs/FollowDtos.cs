namespace MusicXD.Application.DTOs;

public record FollowRequest(Guid FollowingId);

public record FollowerResponse(Guid UserId, string Username, string? AvatarUrl);
