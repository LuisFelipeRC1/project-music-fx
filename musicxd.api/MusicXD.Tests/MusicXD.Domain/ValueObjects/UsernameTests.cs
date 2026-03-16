using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.ValueObjects;

public class UsernameTests
{
    [Fact]
    public void Constructor_ShouldNormalizeValue()
    {
        var username = new Username("  Luis_Felipe  ");

        Assert.Equal("luis_felipe", username.Value);
        Assert.Equal("luis_felipe", username.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("ab")]
    [InlineData("invalid-name")]
    public void Constructor_ShouldThrow_WhenValueIsInvalid(string value)
    {
        var action = () => new Username(value);

        Assert.Throws<ArgumentException>(action);
    }
}
