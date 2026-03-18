using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.SpotifyId)
            .HasConversion(
                spotifyId => spotifyId.Value,
                value => new SpotifyId(value))
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(t => t.SpotifyId).IsUnique();
        builder.Property(t => t.Title).IsRequired().HasMaxLength(500);
        builder.Property(t => t.Source).HasConversion<string>().HasMaxLength(20);
        builder.HasOne<Album>().WithMany(album => album.Tracks).HasForeignKey(t => t.AlbumId);
        builder.HasMany(t => t.Ratings)
            .WithOne()
            .HasForeignKey(rating => rating.TrackId);
    }
}
