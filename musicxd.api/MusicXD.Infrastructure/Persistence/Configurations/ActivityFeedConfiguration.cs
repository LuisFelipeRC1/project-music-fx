using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class ActivityFeedConfiguration : IEntityTypeConfiguration<ActivityFeed>
{
    public void Configure(EntityTypeBuilder<ActivityFeed> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.ActivityType)
            .HasConversion(
                value => value.ToString(),
                value => Enum.Parse<ActivityType>(value))
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(a => a.Payload).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(a => a.UserId);
    }
}
