using MusicXD.Domain.Entities;

namespace MusicXD.Domain.Interfaces;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(Guid reviewId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetByAlbumIdAsync(Guid albumId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Review review, CancellationToken cancellationToken = default);
    Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
}
