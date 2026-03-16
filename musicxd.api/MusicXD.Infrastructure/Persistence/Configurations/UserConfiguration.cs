using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username)
            .HasConversion(
                username => username.Value,
                value => new Username(value))
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => new Email(value))
            .IsRequired()
            .HasMaxLength(256);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.PasswordHash)
            .HasConversion(
                hash => hash.Value,
                value => new PasswordHash(value))
            .IsRequired();
        builder.Property(u => u.Bio).HasMaxLength(1000);
        builder.Property(u => u.ProfileImageUrl).HasMaxLength(2048);
        builder.HasMany(u => u.Following)
            .WithOne()
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.Followers)
            .WithOne()
            .HasForeignKey(f => f.FollowingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
