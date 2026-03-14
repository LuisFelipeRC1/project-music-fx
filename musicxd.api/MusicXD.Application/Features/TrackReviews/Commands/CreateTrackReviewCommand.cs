using MediatR;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;

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
        var review = new TrackReview
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            TrackId = request.TrackId,
            Rating = request.Rating,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.TrackReviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return new TrackReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            TrackId = review.TrackId,
            Rating = review.Rating,
            Content = review.Content,
            CreatedAt = review.CreatedAt
        };
    }
}
