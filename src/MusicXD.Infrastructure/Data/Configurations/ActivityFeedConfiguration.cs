using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Data.Configurations;

public class ActivityFeedConfiguration : IEntityTypeConfiguration<ActivityFeed>
{
    public void Configure(EntityTypeBuilder<ActivityFeed> builder)
    {
        builder.HasKey(a => a.ActivityId);

        builder.HasOne(a => a.User)
            .WithMany(u => u.Activities)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
