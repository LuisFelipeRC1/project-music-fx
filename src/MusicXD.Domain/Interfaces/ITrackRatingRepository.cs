using MusicXD.Domain.Entities;

namespace MusicXD.Domain.Interfaces;

public interface ITrackRatingRepository
{
    Task<TrackRating?> GetAsync(Guid trackId, Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(TrackRating rating, CancellationToken cancellationToken = default);
    Task UpdateAsync(TrackRating rating, CancellationToken cancellationToken = default);
}
