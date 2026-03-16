using Microsoft.EntityFrameworkCore;
using MusicXD.Domain.Entities;

namespace MusicXD.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Artist> Artists { get; }
    DbSet<Album> Albums { get; }
    DbSet<Track> Tracks { get; }
    DbSet<AlbumReview> AlbumReviews { get; }
    DbSet<TrackRating> TrackRatings { get; }
    DbSet<Follow> Follows { get; }
    DbSet<Activity> Activities { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
