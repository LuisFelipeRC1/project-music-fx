using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.SpotifyId).IsRequired().HasMaxLength(100);
        builder.HasIndex(a => a.SpotifyId).IsUnique();
        builder.Property(a => a.Title).IsRequired().HasMaxLength(500);
        builder.HasOne(a => a.Artist).WithMany().HasForeignKey(a => a.ArtistId);
        builder.Property(a => a.Genres)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}
