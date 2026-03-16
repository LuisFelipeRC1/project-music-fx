using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.Entities;

public class AlbumTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var artistId = Guid.NewGuid();

        var album = new Album(
            new SpotifyId("album-1"),
            "  Discovery  ",
            artistId,
            new DateTime(2001, 3, 12),
            "  https://image.test/album.png  ",
            new[] { "Electronic", "electronic", " House " },
            MusicSource.Spotify);

        Assert.Equal("album-1", album.SpotifyId.Value);
        Assert.Equal("Discovery", album.Title);
        Assert.Equal(artistId, album.ArtistId);
        Assert.Equal("https://image.test/album.png", album.CoverImageUrl);
        Assert.Equal(2, album.Genres.Count);
        Assert.Equal(MusicSource.Spotify, album.Source);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenArtistIdIsEmpty()
    {
        var action = () => new Album(new SpotifyId("album-1"), "Discovery", Guid.Empty, DateTime.UtcNow);

        Assert.Throws<ArgumentException>(action);
    }
}
