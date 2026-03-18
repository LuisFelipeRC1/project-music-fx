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
        builder.HasIndex(r => new { r.UserId, r.AlbumId }).IsUnique();
        builder.Property(r => r.Rating)
            .HasConversion(
                rating => rating.Value,
                value => new Rating(value))
            .HasPrecision(3, 1);
        builder.Property(r => r.ReviewText)
            .HasConversion(
                reviewText => reviewText.Value,
                value => new ReviewText(value))
            .IsRequired()
            .HasMaxLength(5000);
        builder.HasOne<User>().WithMany(user => user.AlbumReviews).HasForeignKey(r => r.UserId);
        builder.HasOne<Album>().WithMany(album => album.Reviews).HasForeignKey(r => r.AlbumId);
    }
}
