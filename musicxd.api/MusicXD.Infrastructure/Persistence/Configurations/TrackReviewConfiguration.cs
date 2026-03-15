using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class TrackReviewConfiguration : IEntityTypeConfiguration<TrackReview>
{
    public void Configure(EntityTypeBuilder<TrackReview> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Rating).HasPrecision(3, 1);
        builder.Property(r => r.Content).IsRequired().HasMaxLength(5000);
        builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
        builder.HasOne(r => r.Track).WithMany().HasForeignKey(r => r.TrackId);
    }
}
