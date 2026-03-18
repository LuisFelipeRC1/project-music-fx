using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.ValueObjects;

public class ReviewTextTests
{
    [Fact]
    public void Constructor_ShouldTrimValue()
    {
        var reviewText = new ReviewText("  Great album.  ");

        Assert.Equal("Great album.", reviewText.Value);
        Assert.Equal("Great album.", reviewText.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldThrow_WhenValueIsEmpty(string value)
    {
        var action = () => new ReviewText(value);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenValueIsTooLong()
    {
        var value = new string('a', 5001);

        var action = () => new ReviewText(value);

        Assert.Throws<ArgumentException>(action);
    }
}
