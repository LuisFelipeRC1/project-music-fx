using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class ActivityFeedConfiguration : IEntityTypeConfiguration<ActivityFeed>
{
    public void Configure(EntityTypeBuilder<ActivityFeed> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.EventType).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Payload).IsRequired();
        builder.HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId);
    }
}
