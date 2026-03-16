using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.ValueObjects;

public class PasswordHashTests
{
    [Fact]
    public void Constructor_ShouldTrimValue()
    {
        var hash = new PasswordHash("  hashed-password  ");

        Assert.Equal("hashed-password", hash.Value);
        Assert.Equal("hashed-password", hash.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldThrow_WhenValueIsEmpty(string value)
    {
        var action = () => new PasswordHash(value);

        Assert.Throws<ArgumentException>(action);
    }
}
