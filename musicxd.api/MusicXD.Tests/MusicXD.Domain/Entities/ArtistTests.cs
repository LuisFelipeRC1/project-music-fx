using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using MusicXD.Domain.ValueObjects;

namespace MusicXD.Domain.Tests.Entities;

public class ArtistTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var artist = new Artist(
            new SpotifyId("artist-1"),
            "  Daft Punk  ",
            "  https://image.test/artist.png  ",
            new[] { "Electronic", "electronic", " House " },
            MusicSource.Spotify);

        Assert.Equal("artist-1", artist.SpotifyId.Value);
        Assert.Equal("Daft Punk", artist.Name);
        Assert.Equal("https://image.test/artist.png", artist.ImageUrl);
        Assert.Equal(MusicSource.Spotify, artist.Source);
        Assert.Equal(2, artist.Genres.Count);
    }

    [Fact]
    public void UpdateCatalogDetails_ShouldReplaceProperties()
    {
        var artist = new Artist(new SpotifyId("artist-1"), "Artist");

        artist.UpdateCatalogDetails("  New Artist  ", null, new[] { "pop", "Pop", "rock" }, MusicSource.Manual);

        Assert.Equal("New Artist", artist.Name);
        Assert.Null(artist.ImageUrl);
        Assert.Equal(MusicSource.Manual, artist.Source);
        Assert.Equal(2, artist.Genres.Count);
    }
}
