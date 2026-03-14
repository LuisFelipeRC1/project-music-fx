using Microsoft.EntityFrameworkCore;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Interfaces;
using MusicXD.Infrastructure.Data;

namespace MusicXD.Infrastructure.Repositories;

public class TrackRatingRepository : ITrackRatingRepository
{
    private readonly ApplicationDbContext _context;

    public TrackRatingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackRating?> GetAsync(Guid trackId, Guid userId, CancellationToken cancellationToken = default)
        => await _context.TrackRatings.FirstOrDefaultAsync(
            tr => tr.TrackId == trackId && tr.UserId == userId, cancellationToken);

    public async Task AddAsync(TrackRating rating, CancellationToken cancellationToken = default)
        => await _context.TrackRatings.AddAsync(rating, cancellationToken);

    public Task UpdateAsync(TrackRating rating, CancellationToken cancellationToken = default)
    {
        _context.TrackRatings.Update(rating);
        return Task.CompletedTask;
    }
}
