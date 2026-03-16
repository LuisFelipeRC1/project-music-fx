using MusicXD.Domain.Events;
using MusicXD.Domain.ValueObjects;
using MusicXD.Domain.Entities;

namespace MusicXD.Domain.Tests.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesAndRaiseUserRegisteredEvent()
    {
        var user = new User(
            new Username("Luis_Felipe"),
            new Email("user@example.com"),
            new PasswordHash("hashed-password"),
            "  bio  ",
            "  https://image.test/avatar.png  ");

        Assert.Equal("luis_felipe", user.Username.Value);
        Assert.Equal("user@example.com", user.Email.Value);
        Assert.Equal("hashed-password", user.PasswordHash.Value);
        Assert.Equal("bio", user.Bio);
        Assert.Equal("https://image.test/avatar.png", user.ProfileImageUrl);
        Assert.Single(user.DomainEvents);

        var domainEvent = Assert.IsType<UserRegistered>(user.DomainEvents.Single());
        Assert.Equal(user.Id, domainEvent.UserId);
    }

    [Fact]
    public void UpdateProfile_ShouldNormalizeOptionalValues()
    {
        var user = new User(
            new Username("luis_felipe"),
            new Email("user@example.com"),
            new PasswordHash("hashed-password"));

        user.UpdateProfile("  updated bio  ", "  https://image.test/new.png  ");

        Assert.Equal("updated bio", user.Bio);
        Assert.Equal("https://image.test/new.png", user.ProfileImageUrl);
        Assert.True(user.UpdatedAt >= user.CreatedAt);
    }

    [Fact]
    public void ChangePassword_ShouldReplacePasswordHash()
    {
        var user = new User(
            new Username("luis_felipe"),
            new Email("user@example.com"),
            new PasswordHash("old-hash"));

        user.ChangePassword(new PasswordHash("new-hash"));

        Assert.Equal("new-hash", user.PasswordHash.Value);
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveRaisedEvents()
    {
        var user = new User(
            new Username("luis_felipe"),
            new Email("user@example.com"),
            new PasswordHash("hashed-password"));

        user.ClearDomainEvents();

        Assert.Empty(user.DomainEvents);
    }
}
