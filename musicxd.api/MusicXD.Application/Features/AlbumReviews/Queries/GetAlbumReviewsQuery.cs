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
            select new { review, user })
            .ToListAsync(cancellationToken);

        return reviews.Select(result => new AlbumReviewDto
        {
            Id = result.review.Id,
            UserId = result.review.UserId,
            Username = result.user.Username.Value,
            AlbumId = result.review.AlbumId,
            Rating = result.review.Rating.Value,
            Content = result.review.ReviewText.Value,
            CreatedAt = result.review.CreatedAt
        });
    }
}
