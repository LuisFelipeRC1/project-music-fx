using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Data.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.ArtistId);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(256);
        builder.Property(a => a.SpotifyId).HasMaxLength(50);
        builder.HasIndex(a => a.SpotifyId);
    }
}
