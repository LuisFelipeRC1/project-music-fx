using MusicXD.Domain.Entities;

namespace MusicXD.Domain.Interfaces;

public interface IActivityFeedRepository
{
    Task<IEnumerable<ActivityFeed>> GetFeedAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(ActivityFeed activity, CancellationToken cancellationToken = default);
}
