using MusicXD.Application.DTOs;

namespace MusicXD.Application.Common.Interfaces;

public interface IActivityFeedService
{
    Task<IEnumerable<ActivityFeedResponse>> GetFeedAsync(Guid userId, CancellationToken cancellationToken = default);
}
