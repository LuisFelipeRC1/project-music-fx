using MediatR;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;
using ActivityFeedEntry = MusicXD.Domain.Entities.ActivityFeed;

namespace MusicXD.Application.Features.AlbumReviews.Commands;

public record CreateAlbumReviewCommand(Guid UserId, Guid AlbumId, decimal Rating, string Content) : IRequest<AlbumReviewDto>;

public class CreateAlbumReviewCommandHandler : IRequestHandler<CreateAlbumReviewCommand, AlbumReviewDto>
{
    private readonly IApplicationDbContext _context;

    public CreateAlbumReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AlbumReviewDto> Handle(CreateAlbumReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new AlbumReview(
            request.UserId,
            request.AlbumId,
            new RatingScore(request.Rating),
            request.Content);

        _context.AlbumReviews.Add(review);
        _context.ActivityFeeds.Add(new ActivityFeedEntry(
            request.UserId,
            ActivityType.AlbumReviewed,
            review.Content));
        await _context.SaveChangesAsync(cancellationToken);

        return new AlbumReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            AlbumId = review.AlbumId,
            Rating = review.Rating.Value,
            Content = review.Content,
            CreatedAt = review.CreatedAt
        };
    }
}
