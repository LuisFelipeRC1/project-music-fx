using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;
using System.Text.Json;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.SpotifyId)
            .HasConversion(
                spotifyId => spotifyId.Value,
                value => new SpotifyId(value))
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(a => a.SpotifyId).IsUnique();
        builder.Property(a => a.Name).IsRequired().HasMaxLength(500);
        builder.Property(a => a.ImageUrl).HasMaxLength(2048);
        builder.Property(a => a.Source).HasConversion<string>().HasMaxLength(20);
        builder.Property(a => a.Genres)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());
        builder.HasMany(a => a.Albums)
            .WithOne()
            .HasForeignKey(album => album.ArtistId);
    }
}
