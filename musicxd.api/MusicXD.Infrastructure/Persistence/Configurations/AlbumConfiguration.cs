using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;
using System.Text.Json;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.SpotifyId)
            .HasConversion(
                spotifyId => spotifyId.Value,
                value => new SpotifyId(value))
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(a => a.SpotifyId).IsUnique();
        builder.Property(a => a.Title).IsRequired().HasMaxLength(500);
        builder.Property(a => a.CoverImageUrl).HasMaxLength(2048);
        builder.Property(a => a.Source).HasConversion<string>().HasMaxLength(20);
        builder.HasOne<Artist>().WithMany(artist => artist.Albums).HasForeignKey(a => a.ArtistId);
        builder.Property(a => a.Genres)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());
        builder.HasMany(a => a.Reviews)
            .WithOne()
            .HasForeignKey(review => review.AlbumId);
        builder.HasMany(a => a.Tracks)
            .WithOne()
            .HasForeignKey(track => track.AlbumId);
    }
}
