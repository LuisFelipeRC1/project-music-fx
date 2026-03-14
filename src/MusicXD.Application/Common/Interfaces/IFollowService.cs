using MusicXD.Application.DTOs;

namespace MusicXD.Application.Common.Interfaces;

public interface IFollowService
{
    Task FollowAsync(Guid followerId, FollowRequest request, CancellationToken cancellationToken = default);
    Task UnfollowAsync(Guid followerId, Guid followingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<FollowerResponse>> GetFollowersAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<FollowerResponse>> GetFollowingAsync(Guid userId, CancellationToken cancellationToken = default);
}
