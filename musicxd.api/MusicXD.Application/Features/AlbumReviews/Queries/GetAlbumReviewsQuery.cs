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
            from review in _context.AlbumReviews
            join user in _context.Users on review.UserId equals user.Id
            where review.AlbumId == request.AlbumId
            select new
            {
                Review = review,
                Username = user.Username
            })
            .ToListAsync(cancellationToken);

        return reviews.Select(item => new AlbumReviewDto
        {
            Id = item.Review.Id,
            UserId = item.Review.UserId,
            Username = item.Username,
            AlbumId = item.Review.AlbumId,
            Rating = item.Review.Rating.Value,
            Content = item.Review.Content,
            CreatedAt = item.Review.CreatedAt
        });
    }
}
