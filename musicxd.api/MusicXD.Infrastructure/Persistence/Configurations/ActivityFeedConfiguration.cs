using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Type)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(a => a.Payload).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(a => a.UserId);
    }
}
