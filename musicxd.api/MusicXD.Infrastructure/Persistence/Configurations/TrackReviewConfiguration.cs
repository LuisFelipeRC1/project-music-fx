using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class TrackRatingConfiguration : IEntityTypeConfiguration<TrackRating>
{
    public void Configure(EntityTypeBuilder<TrackRating> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => new { r.UserId, r.TrackId }).IsUnique();
        builder.Property(r => r.Rating)
            .HasConversion(
                rating => rating.Value,
                value => new Rating(value))
            .HasPrecision(3, 1);
        builder.HasOne<User>().WithMany(user => user.TrackRatings).HasForeignKey(r => r.UserId);
        builder.HasOne<Track>().WithMany(track => track.Ratings).HasForeignKey(r => r.TrackId);
    }
}
