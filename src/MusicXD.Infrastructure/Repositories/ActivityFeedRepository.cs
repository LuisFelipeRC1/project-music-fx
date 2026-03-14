using Microsoft.EntityFrameworkCore;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Interfaces;
using MusicXD.Infrastructure.Data;

namespace MusicXD.Infrastructure.Repositories;

public class ActivityFeedRepository : IActivityFeedRepository
{
    private readonly ApplicationDbContext _context;

    public ActivityFeedRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ActivityFeed>> GetFeedAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.ActivityFeeds
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .Take(50)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(ActivityFeed activity, CancellationToken cancellationToken = default)
        => await _context.ActivityFeeds.AddAsync(activity, cancellationToken);
}
