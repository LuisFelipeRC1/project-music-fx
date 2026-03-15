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
        var reviews = await (
            from review in _context.AlbumReviews.AsNoTracking()
            join user in _context.Users on review.UserId equals user.Id
            where review.AlbumId == request.AlbumId
            select new AlbumReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                Username = user.Username,
                AlbumId = review.AlbumId,
                Rating = review.Rating.Value,
                Content = review.Content,
                CreatedAt = review.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return reviews;
    }
}
