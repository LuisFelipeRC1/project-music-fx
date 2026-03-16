using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.ValueObjects;

public class RatingTests
{
    [Fact]
    public void Constructor_ShouldRoundToOneDecimal()
    {
        var rating = new Rating(4.45m);

        Assert.Equal(4.5m, rating.Value);
        Assert.Equal("4.5", rating.ToString());
    }

    [Theory]
    [InlineData(0.9)]
    [InlineData(5.1)]
    public void Constructor_ShouldThrow_WhenValueIsOutOfRange(decimal value)
    {
        var action = () => new Rating(value);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }
}
