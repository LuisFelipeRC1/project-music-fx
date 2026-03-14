using MusicXD.Domain.Entities;

namespace MusicXD.Domain.Interfaces;

public interface IFollowRepository
{
    Task<Follow?> GetAsync(Guid followerId, Guid followingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Follow>> GetFollowersAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Follow>> GetFollowingAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Follow follow, CancellationToken cancellationToken = default);
    Task RemoveAsync(Follow follow, CancellationToken cancellationToken = default);
}
