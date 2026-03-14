using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Data.Configurations;

public class TrackRatingConfiguration : IEntityTypeConfiguration<TrackRating>
{
    public void Configure(EntityTypeBuilder<TrackRating> builder)
    {
        builder.HasKey(tr => new { tr.TrackId, tr.UserId });
        builder.Property(tr => tr.Rating).IsRequired();

        builder.HasOne(tr => tr.Track)
            .WithMany(t => t.Ratings)
            .HasForeignKey(tr => tr.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tr => tr.User)
            .WithMany(u => u.TrackRatings)
            .HasForeignKey(tr => tr.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
