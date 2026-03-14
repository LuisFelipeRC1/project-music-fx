using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.Interfaces;

namespace MusicXD.Application.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;
    private readonly IActivityFeedRepository _activityFeedRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FollowService(
        IFollowRepository followRepository,
        IUserRepository userRepository,
        IActivityFeedRepository activityFeedRepository,
        IUnitOfWork unitOfWork)
    {
        _followRepository = followRepository;
        _userRepository = userRepository;
        _activityFeedRepository = activityFeedRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task FollowAsync(Guid followerId, FollowRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await _followRepository.GetAsync(followerId, request.FollowingId, cancellationToken);
        if (existing is not null)
            throw new InvalidOperationException("Already following this user.");

        var follow = Follow.Create(followerId, request.FollowingId);
        await _followRepository.AddAsync(follow, cancellationToken);

        var activity = ActivityFeed.Create(followerId, ActivityType.FollowUser, request.FollowingId);
        await _activityFeedRepository.AddAsync(activity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnfollowAsync(Guid followerId, Guid followingId, CancellationToken cancellationToken = default)
    {
        var follow = await _followRepository.GetAsync(followerId, followingId, cancellationToken)
            ?? throw new KeyNotFoundException("Follow relationship not found.");

        await _followRepository.RemoveAsync(follow, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<FollowerResponse>> GetFollowersAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var follows = await _followRepository.GetFollowersAsync(userId, cancellationToken);
        var responses = new List<FollowerResponse>();

        foreach (var follow in follows)
        {
            var user = await _userRepository.GetByIdAsync(follow.FollowerId, cancellationToken);
            if (user is not null)
                responses.Add(new FollowerResponse(user.UserId, user.Username, user.AvatarUrl));
        }

        return responses;
    }

    public async Task<IEnumerable<FollowerResponse>> GetFollowingAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var follows = await _followRepository.GetFollowingAsync(userId, cancellationToken);
        var responses = new List<FollowerResponse>();

        foreach (var follow in follows)
        {
            var user = await _userRepository.GetByIdAsync(follow.FollowingId, cancellationToken);
            if (user is not null)
                responses.Add(new FollowerResponse(user.UserId, user.Username, user.AvatarUrl));
        }

        return responses;
    }
}
