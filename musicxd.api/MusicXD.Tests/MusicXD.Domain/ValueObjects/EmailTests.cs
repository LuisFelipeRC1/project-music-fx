using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Constructor_ShouldNormalizeValue()
    {
        var email = new Email("  USER@Example.COM  ");

        Assert.Equal("user@example.com", email.Value);
        Assert.Equal("user@example.com", email.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("invalid-email")]
    public void Constructor_ShouldThrow_WhenValueIsInvalid(string value)
    {
        var action = () => new Email(value);

        Assert.Throws<ArgumentException>(action);
    }
}
