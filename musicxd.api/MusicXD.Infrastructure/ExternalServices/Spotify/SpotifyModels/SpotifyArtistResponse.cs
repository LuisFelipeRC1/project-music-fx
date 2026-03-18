using System.Text.Json.Serialization;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

public class SpotifyArtistResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; } = new();

    [JsonPropertyName("images")]
    public List<SpotifyImage> Images { get; set; } = new();
}
