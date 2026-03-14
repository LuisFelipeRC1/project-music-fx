using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Data.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(a => a.AlbumId);
        builder.Property(a => a.Title).IsRequired().HasMaxLength(256);
        builder.Property(a => a.SpotifyId).HasMaxLength(50);
        builder.Property(a => a.CoverUrl).HasMaxLength(1024);
        builder.HasIndex(a => a.SpotifyId);

        builder.HasOne(a => a.Artist)
            .WithMany(ar => ar.Albums)
            .HasForeignKey(a => a.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
