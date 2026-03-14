using MusicXD.Application.Common.Interfaces;
using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.Interfaces;

namespace MusicXD.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ITrackRatingRepository _trackRatingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IActivityFeedRepository _activityFeedRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(
        IReviewRepository reviewRepository,
        ITrackRatingRepository trackRatingRepository,
        IUserRepository userRepository,
        IActivityFeedRepository activityFeedRepository,
        IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _trackRatingRepository = trackRatingRepository;
        _userRepository = userRepository;
        _activityFeedRepository = activityFeedRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReviewResponse> CreateReviewAsync(Guid userId, CreateReviewRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new KeyNotFoundException($"User {userId} not found.");

        var review = Review.Create(request.AlbumId, userId, request.Rating, request.ReviewText);
        await _reviewRepository.AddAsync(review, cancellationToken);

        var activity = ActivityFeed.Create(userId, ActivityType.ReviewCreated, review.ReviewId);
        await _activityFeedRepository.AddAsync(activity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReviewResponse(
            review.ReviewId,
            review.AlbumId,
            review.UserId,
            user.Username,
            review.Rating,
            review.ReviewText,
            review.CreatedAt);
    }

    public async Task<IEnumerable<ReviewResponse>> GetAlbumReviewsAsync(Guid albumId, CancellationToken cancellationToken = default)
    {
        var reviews = await _reviewRepository.GetByAlbumIdAsync(albumId, cancellationToken);
        var responses = new List<ReviewResponse>();

        foreach (var review in reviews)
        {
            var user = await _userRepository.GetByIdAsync(review.UserId, cancellationToken);
            responses.Add(new ReviewResponse(
                review.ReviewId,
                review.AlbumId,
                review.UserId,
                user?.Username ?? "Unknown",
                review.Rating,
                review.ReviewText,
                review.CreatedAt));
        }

        return responses;
    }

    public async Task<TrackRatingResponse> RateTrackAsync(Guid userId, CreateTrackRatingRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await _trackRatingRepository.GetAsync(request.TrackId, userId, cancellationToken);

        if (existing is not null)
        {
            var updated = TrackRating.Create(request.TrackId, userId, request.Rating);
            await _trackRatingRepository.UpdateAsync(updated, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new TrackRatingResponse(updated.TrackId, updated.UserId, updated.Rating, updated.CreatedAt);
        }

        var rating = TrackRating.Create(request.TrackId, userId, request.Rating);
        await _trackRatingRepository.AddAsync(rating, cancellationToken);

        var activity = ActivityFeed.Create(userId, ActivityType.TrackRated, request.TrackId);
        await _activityFeedRepository.AddAsync(activity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new TrackRatingResponse(rating.TrackId, rating.UserId, rating.Rating, rating.CreatedAt);
    }
}
