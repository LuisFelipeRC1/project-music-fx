using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;
using MusicXD.Domain.Interfaces;

namespace MusicXD.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFollowRepository _followRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IFollowRepository followRepository,
        IReviewRepository reviewRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _followRepository = followRepository;
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserProfileResponse> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new KeyNotFoundException($"User {userId} not found.");

        var reviews = await _reviewRepository.GetByUserIdAsync(userId, cancellationToken);
        var followers = await _followRepository.GetFollowersAsync(userId, cancellationToken);
        var following = await _followRepository.GetFollowingAsync(userId, cancellationToken);

        return new UserProfileResponse(
            user.UserId,
            user.Username,
            user.Email,
            user.Bio,
            user.AvatarUrl,
            reviews.Count(),
            followers.Count(),
            following.Count());
    }

    public async Task<UserProfileResponse> UpdateProfileAsync(Guid userId, UpdateProfileRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new KeyNotFoundException($"User {userId} not found.");

        user.UpdateProfile(request.Bio, request.AvatarUrl);
        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetProfileAsync(userId, cancellationToken);
    }

    public async Task<IEnumerable<UserProfileResponse>> SearchUsersAsync(string query, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.SearchAsync(query, cancellationToken);
        var results = new List<UserProfileResponse>();

        foreach (var user in users)
        {
            var reviews = await _reviewRepository.GetByUserIdAsync(user.UserId, cancellationToken);
            var followers = await _followRepository.GetFollowersAsync(user.UserId, cancellationToken);
            var following = await _followRepository.GetFollowingAsync(user.UserId, cancellationToken);

            results.Add(new UserProfileResponse(
                user.UserId,
                user.Username,
                user.Email,
                user.Bio,
                user.AvatarUrl,
                reviews.Count(),
                followers.Count(),
                following.Count()));
        }

        return results;
    }
}
