using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;

namespace MusicXD.Application.Features.AlbumReviews.Queries;

public record GetAlbumReviewsQuery(Guid AlbumId) : IRequest<IEnumerable<AlbumReviewDto>>;

public class GetAlbumReviewsQueryHandler : IRequestHandler<GetAlbumReviewsQuery, IEnumerable<AlbumReviewDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAlbumReviewsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AlbumReviewDto>> Handle(GetAlbumReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _context.AlbumReviews
            .Include(r => r.User)
            .Where(r => r.AlbumId == request.AlbumId)
            .Select(r => new AlbumReviewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                Username = r.User.Username,
                AlbumId = r.AlbumId,
                Rating = r.Rating,
                Content = r.Content,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
