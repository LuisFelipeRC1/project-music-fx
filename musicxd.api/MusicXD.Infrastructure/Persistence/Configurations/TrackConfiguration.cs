using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.SpotifyId).IsRequired().HasMaxLength(100);
        builder.HasIndex(t => t.SpotifyId).IsUnique();
        builder.Property(t => t.Title).IsRequired().HasMaxLength(500);
        builder.HasOne(t => t.Album).WithMany().HasForeignKey(t => t.AlbumId);
    }
}
