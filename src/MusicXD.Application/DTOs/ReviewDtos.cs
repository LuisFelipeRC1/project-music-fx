namespace MusicXD.Application.DTOs;

public record CreateReviewRequest(Guid AlbumId, int Rating, string? ReviewText);

public record ReviewResponse(
    Guid ReviewId,
    Guid AlbumId,
    Guid UserId,
    string Username,
    int Rating,
    string? ReviewText,
    DateTime CreatedAt);

public record CreateTrackRatingRequest(Guid TrackId, int Rating);

public record TrackRatingResponse(Guid TrackId, Guid UserId, int Rating, DateTime CreatedAt);
