using Microsoft.EntityFrameworkCore;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Interfaces;
using MusicXD.Infrastructure.Data;

namespace MusicXD.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Review?> GetByIdAsync(Guid reviewId, CancellationToken cancellationToken = default)
        => await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewId == reviewId, cancellationToken);

    public async Task<IEnumerable<Review>> GetByAlbumIdAsync(Guid albumId, CancellationToken cancellationToken = default)
        => await _context.Reviews.Where(r => r.AlbumId == albumId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Reviews.Where(r => r.UserId == userId).ToListAsync(cancellationToken);

    public async Task AddAsync(Review review, CancellationToken cancellationToken = default)
        => await _context.Reviews.AddAsync(review, cancellationToken);

    public Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
    {
        _context.Reviews.Update(review);
        return Task.CompletedTask;
    }
}
