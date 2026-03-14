namespace MusicXD.Application.DTOs;

public record RegisterRequest(string Username, string Email, string Password);

public record LoginRequest(string Email, string Password);

public record AuthResponse(string Token, string Username, Guid UserId);

public record UserProfileResponse(
    Guid UserId,
    string Username,
    string Email,
    string? Bio,
    string? AvatarUrl,
    int ReviewCount,
    int FollowersCount,
    int FollowingCount);

public record UpdateProfileRequest(string? Bio, string? AvatarUrl);
