using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;

namespace MusicXD.Application.Features.ActivityFeed.Queries;

public record GetActivityFeedQuery(Guid UserId) : IRequest<IEnumerable<ActivityFeedDto>>;

public class GetActivityFeedQueryHandler : IRequestHandler<GetActivityFeedQuery, IEnumerable<ActivityFeedDto>>
{
    private readonly IApplicationDbContext _context;

    public GetActivityFeedQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ActivityFeedDto>> Handle(GetActivityFeedQuery request, CancellationToken cancellationToken)
    {
        var activities = await _context.Activities
            .AsNoTracking()
            .Where(a => a.UserId == request.UserId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return activities.Select(activity => new ActivityFeedDto
        {
            Id = activity.Id,
            UserId = activity.UserId,
            EventType = activity.Type.ToString(),
            Payload = activity.Payload,
            CreatedAt = activity.CreatedAt
        });
    }
}
