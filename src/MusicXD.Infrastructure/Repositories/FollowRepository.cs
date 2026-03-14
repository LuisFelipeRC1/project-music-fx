using Microsoft.EntityFrameworkCore;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Interfaces;
using MusicXD.Infrastructure.Data;

namespace MusicXD.Infrastructure.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly ApplicationDbContext _context;

    public FollowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Follow?> GetAsync(Guid followerId, Guid followingId, CancellationToken cancellationToken = default)
        => await _context.Follows.FirstOrDefaultAsync(
            f => f.FollowerId == followerId && f.FollowingId == followingId, cancellationToken);

    public async Task<IEnumerable<Follow>> GetFollowersAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Follows.Where(f => f.FollowingId == userId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Follow>> GetFollowingAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Follows.Where(f => f.FollowerId == userId).ToListAsync(cancellationToken);

    public async Task AddAsync(Follow follow, CancellationToken cancellationToken = default)
        => await _context.Follows.AddAsync(follow, cancellationToken);

    public Task RemoveAsync(Follow follow, CancellationToken cancellationToken = default)
    {
        _context.Follows.Remove(follow);
        return Task.CompletedTask;
    }
}
