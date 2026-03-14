using Microsoft.EntityFrameworkCore;
using MusicXD.Domain.Entities;
using MusicXD.Infrastructure.Data.Configurations;

namespace MusicXD.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Artist> Artists { get; set; } = null!;
    public DbSet<Album> Albums { get; set; } = null!;
    public DbSet<Track> Tracks { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<TrackRating> TrackRatings { get; set; } = null!;
    public DbSet<Follow> Follows { get; set; } = null!;
    public DbSet<ActivityFeed> ActivityFeeds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
