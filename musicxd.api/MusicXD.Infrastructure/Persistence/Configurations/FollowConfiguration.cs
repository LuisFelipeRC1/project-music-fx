using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.HasKey(f => f.Id);
        builder.HasIndex(f => new { f.FollowerId, f.FollowingId }).IsUnique();
        builder.HasOne<User>().WithMany(user => user.Following).HasForeignKey(f => f.FollowerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>().WithMany(user => user.Followers).HasForeignKey(f => f.FollowingId).OnDelete(DeleteBehavior.Restrict);
    }
}
