using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.Entities;

public class TrackTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var albumId = Guid.NewGuid();

        var track = new Track(
            new SpotifyId("track-1"),
            "  One More Time  ",
            albumId,
            320000,
            1,
            MusicSource.Spotify);

        Assert.Equal("track-1", track.SpotifyId.Value);
        Assert.Equal("One More Time", track.Title);
        Assert.Equal(albumId, track.AlbumId);
        Assert.Equal(320000, track.DurationMs);
        Assert.Equal(1, track.TrackNumber);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1000, 0)]
    public void Constructor_ShouldThrow_WhenNumericValuesAreInvalid(int durationMs, int trackNumber)
    {
        var action = () => new Track(
            new SpotifyId("track-1"),
            "Track",
            Guid.NewGuid(),
            durationMs,
            trackNumber,
            MusicSource.Spotify);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }
}
