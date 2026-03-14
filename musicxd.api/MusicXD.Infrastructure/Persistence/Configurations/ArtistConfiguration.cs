using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.SpotifyId).IsRequired().HasMaxLength(100);
        builder.HasIndex(a => a.SpotifyId).IsUnique();
        builder.Property(a => a.Name).IsRequired().HasMaxLength(500);
        builder.Property(a => a.Genres)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}
