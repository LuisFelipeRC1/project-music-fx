using MusicXD.Application.DTOs;

namespace MusicXD.Application.Common.Interfaces;

public interface IReviewService
{
    Task<ReviewResponse> CreateReviewAsync(Guid userId, CreateReviewRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<ReviewResponse>> GetAlbumReviewsAsync(Guid albumId, CancellationToken cancellationToken = default);
    Task<TrackRatingResponse> RateTrackAsync(Guid userId, CreateTrackRatingRequest request, CancellationToken cancellationToken = default);
}
