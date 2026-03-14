using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;
using MusicXD.Domain.Interfaces;

namespace MusicXD.Application.Services;

public class ActivityFeedService : IActivityFeedService
{
    private readonly IActivityFeedRepository _activityFeedRepository;
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;

    public ActivityFeedService(
        IActivityFeedRepository activityFeedRepository,
        IFollowRepository followRepository,
        IUserRepository userRepository)
    {
        _activityFeedRepository = activityFeedRepository;
        _followRepository = followRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ActivityFeedResponse>> GetFeedAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var following = await _followRepository.GetFollowingAsync(userId, cancellationToken);
        var responses = new List<ActivityFeedResponse>();

        foreach (var follow in following)
        {
            var activities = await _activityFeedRepository.GetFeedAsync(follow.FollowingId, cancellationToken);
            var user = await _userRepository.GetByIdAsync(follow.FollowingId, cancellationToken);

            foreach (var activity in activities)
            {
                responses.Add(new ActivityFeedResponse(
                    activity.ActivityId,
                    activity.UserId,
                    user?.Username ?? "Unknown",
                    activity.ActivityType.ToString(),
                    activity.TargetId,
                    activity.CreatedAt));
            }
        }

        return responses.OrderByDescending(a => a.CreatedAt);
    }
}
