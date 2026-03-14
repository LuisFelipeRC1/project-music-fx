using Microsoft.EntityFrameworkCore;
using MusicXD.Application.Interfaces;
using MusicXD.Domain.Entities;
using MusicXD.Infrastructure.Persistence.Configurations;

namespace MusicXD.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<AlbumReview> AlbumReviews => Set<AlbumReview>();
    public DbSet<TrackReview> TrackReviews => Set<TrackReview>();
    public DbSet<Follow> Follows => Set<Follow>();
    public DbSet<ActivityFeed> ActivityFeeds => Set<ActivityFeed>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ArtistConfiguration());
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new TrackConfiguration());
        modelBuilder.ApplyConfiguration(new AlbumReviewConfiguration());
        modelBuilder.ApplyConfiguration(new TrackReviewConfiguration());
        modelBuilder.ApplyConfiguration(new FollowConfiguration());
        modelBuilder.ApplyConfiguration(new ActivityFeedConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
