using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;
using MusicXD.Application.Mapper;

namespace MusicXD.Application.Features.TrackRatings.Queries;

public record GetTrackRatingsQuery(Guid TrackId) : IRequest<IEnumerable<TrackRatingDto>>;

public class GetTrackRatingsQueryHandler : IRequestHandler<GetTrackRatingsQuery, IEnumerable<TrackRatingDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTrackRatingsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrackRatingDto>> Handle(GetTrackRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await (
            from rating in _context.TrackRatings.AsNoTracking()
            join user in _context.Users on rating.UserId equals user.Id
            where rating.TrackId == request.TrackId
            select new { rating, user })
            .ToListAsync(cancellationToken);

        return ratings.Select(result => result.rating.ToTrackRatingDto(result.user.Username.Value));
    }
}
