using System.Text.Json.Serialization;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

public class SpotifyAlbumResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }

    [JsonPropertyName("release_date_precision")]
    public string? ReleaseDatePrecision { get; set; }

    [JsonPropertyName("images")]
    public List<SpotifyImage> Images { get; set; } = new();

    [JsonPropertyName("artists")]
    public List<SpotifyArtistSummary> Artists { get; set; } = new();

    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; } = new();
}
