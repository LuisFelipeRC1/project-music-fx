using MediatR;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;
using ActivityFeedEntry = MusicXD.Domain.Entities.ActivityFeed;

namespace MusicXD.Application.Features.TrackReviews.Commands;

public record CreateTrackReviewCommand(Guid UserId, Guid TrackId, decimal Rating, string Content) : IRequest<TrackReviewDto>;

public class CreateTrackReviewCommandHandler : IRequestHandler<CreateTrackReviewCommand, TrackReviewDto>
{
    private readonly IApplicationDbContext _context;

    public CreateTrackReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackReviewDto> Handle(CreateTrackReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new TrackReview(
            request.UserId,
            request.TrackId,
            new RatingScore(request.Rating),
            request.Content);

        _context.TrackReviews.Add(review);
        _context.ActivityFeeds.Add(new ActivityFeedEntry(
            request.UserId,
            ActivityType.TrackRated,
            review.Content));
        await _context.SaveChangesAsync(cancellationToken);

        return new TrackReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            TrackId = review.TrackId,
            Rating = review.Rating.Value,
            Content = review.Content,
            CreatedAt = review.CreatedAt
        };
    }
}
