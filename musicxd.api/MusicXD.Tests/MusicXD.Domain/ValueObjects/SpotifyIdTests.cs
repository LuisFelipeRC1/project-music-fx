using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.ValueObjects;

public class SpotifyIdTests
{
    [Fact]
    public void Constructor_ShouldTrimValue()
    {
        var spotifyId = new SpotifyId("  123abc  ");

        Assert.Equal("123abc", spotifyId.Value);
        Assert.Equal("123abc", spotifyId.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldThrow_WhenValueIsEmpty(string value)
    {
        var action = () => new SpotifyId(value);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsTooLong()
    {
        var value = new string('a', 101);

        var action = () => new SpotifyId(value);

        Assert.Throws<ArgumentException>(action);
    }
}
