using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class AlbumReviewConfiguration : IEntityTypeConfiguration<AlbumReview>
{
    public void Configure(EntityTypeBuilder<AlbumReview> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Rating)
            .HasConversion(
                rating => rating.Value,
                value => new RatingScore(value))
            .HasPrecision(3, 1);
        builder.Property(r => r.Content).IsRequired().HasMaxLength(5000);
        builder.HasOne<User>().WithMany().HasForeignKey(r => r.UserId);
        builder.HasOne<Album>().WithMany().HasForeignKey(r => r.AlbumId);
    }
}
