using System.Text.Json.Serialization;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

public class SpotifyTrackResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; set; }

    [JsonPropertyName("track_number")]
    public int TrackNumber { get; set; }

    [JsonPropertyName("album")]
    public SpotifyAlbumSummary? Album { get; set; }

    [JsonPropertyName("artists")]
    public List<SpotifyArtistSummary> Artists { get; set; } = new();
}
