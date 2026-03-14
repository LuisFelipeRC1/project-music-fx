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
        return await _context.TrackReviews
            .Include(r => r.User)
            .Where(r => r.TrackId == request.TrackId)
            .Select(r => new TrackReviewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                Username = r.User.Username,
                TrackId = r.TrackId,
                Rating = r.Rating,
                Content = r.Content,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
