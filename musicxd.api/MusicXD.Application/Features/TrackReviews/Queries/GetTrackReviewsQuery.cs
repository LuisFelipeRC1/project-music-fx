using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;

namespace MusicXD.Application.Features.TrackReviews.Queries;

public record GetTrackReviewsQuery(Guid TrackId) : IRequest<IEnumerable<TrackReviewDto>>;

public class GetTrackReviewsQueryHandler : IRequestHandler<GetTrackReviewsQuery, IEnumerable<TrackReviewDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTrackReviewsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrackReviewDto>> Handle(GetTrackReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await (
            from review in _context.TrackReviews
            join user in _context.Users on review.UserId equals user.Id
            where review.TrackId == request.TrackId
            select new
            {
                Review = review,
                Username = user.Username
            })
            .ToListAsync(cancellationToken);

        return reviews.Select(item => new TrackReviewDto
        {
            Id = item.Review.Id,
            UserId = item.Review.UserId,
            Username = item.Username,
            TrackId = item.Review.TrackId,
            Rating = item.Review.Rating.Value,
            Content = item.Review.Content,
            CreatedAt = item.Review.CreatedAt
        });
    }
}
