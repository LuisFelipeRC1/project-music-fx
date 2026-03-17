using System.Text.Json.Serialization;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

public class SpotifyImage
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }
}

public class SpotifyArtistSummary
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class SpotifyAlbumSummary
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public List<SpotifyImage> Images { get; set; } = new();
}

public class SpotifyPagingResponse<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; } = new();
}

public class SpotifyArtistSearchResponse
{
    [JsonPropertyName("artists")]
    public SpotifyPagingResponse<SpotifyArtistResponse>? Artists { get; set; }
}

public class SpotifyAlbumSearchResponse
{
    [JsonPropertyName("albums")]
    public SpotifyPagingResponse<SpotifyAlbumResponse>? Albums { get; set; }
}

public class SpotifyTrackSearchResponse
{
    [JsonPropertyName("tracks")]
    public SpotifyPagingResponse<SpotifyTrackResponse>? Tracks { get; set; }
}

public class SpotifyTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}
