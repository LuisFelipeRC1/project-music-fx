using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyAuthService;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyClient;

public sealed class SpotifyClient : ISpotifyClient
{
    private const int MaxSearchLimit = 10;

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient;
    private readonly ISpotifyAuthService _spotifyAuthService;

    public SpotifyClient(HttpClient httpClient, ISpotifyAuthService spotifyAuthService)
    {
        _httpClient = httpClient;
        _spotifyAuthService = spotifyAuthService;
    }

    public Task<SpotifyArtistResponse> GetArtistAsync(string artistId, CancellationToken cancellationToken = default)
    {
        ValidateIdentifier(artistId, nameof(artistId));
        return GetAsync<SpotifyArtistResponse>($"artists/{Uri.EscapeDataString(artistId)}", cancellationToken);
    }

    public Task<SpotifyAlbumResponse> GetAlbumAsync(string albumId, string? market = null, CancellationToken cancellationToken = default)
    {
        ValidateIdentifier(albumId, nameof(albumId));
        return GetAsync<SpotifyAlbumResponse>(BuildRelativePath($"albums/{Uri.EscapeDataString(albumId)}", market), cancellationToken);
    }

    public Task<SpotifyTrackResponse> GetTrackAsync(string trackId, string? market = null, CancellationToken cancellationToken = default)
    {
        ValidateIdentifier(trackId, nameof(trackId));
        return GetAsync<SpotifyTrackResponse>(BuildRelativePath($"tracks/{Uri.EscapeDataString(trackId)}", market), cancellationToken);
    }

    public async Task<IReadOnlyList<SpotifyArtistResponse>> SearchArtistsAsync(
        string query,
        int limit = MaxSearchLimit,
        string? market = null,
        CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<SpotifyArtistSearchResponse>(BuildSearchPath(query, "artist", limit, market), cancellationToken);
        return response.Artists?.Items ?? new List<SpotifyArtistResponse>();
    }

    public async Task<IReadOnlyList<SpotifyAlbumResponse>> SearchAlbumsAsync(
        string query,
        int limit = MaxSearchLimit,
        string? market = null,
        CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<SpotifyAlbumSearchResponse>(BuildSearchPath(query, "album", limit, market), cancellationToken);
        return response.Albums?.Items ?? new List<SpotifyAlbumResponse>();
    }

    public async Task<IReadOnlyList<SpotifyTrackResponse>> SearchTracksAsync(
        string query,
        int limit = MaxSearchLimit,
        string? market = null,
        CancellationToken cancellationToken = default)
    {
        var response = await GetAsync<SpotifyTrackSearchResponse>(BuildSearchPath(query, "track", limit, market), cancellationToken);
        return response.Tracks?.Items ?? new List<SpotifyTrackResponse>();
    }

    private async Task<TResponse> GetAsync<TResponse>(string relativePath, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, relativePath);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _spotifyAuthService.GetAccessTokenAsync(cancellationToken));

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>(SerializerOptions, cancellationToken)
            ?? throw new InvalidOperationException($"Spotify returned an empty payload for '{relativePath}'.");
    }

    private static string BuildRelativePath(string path, string? market)
    {
        if (string.IsNullOrWhiteSpace(market))
        {
            return path;
        }

        return $"{path}?market={Uri.EscapeDataString(market)}";
    }

    private static string BuildSearchPath(string query, string type, int limit, string? market)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException("Search query must be provided.", nameof(query));
        }

        if (limit is < 1 or > MaxSearchLimit)
        {
            throw new ArgumentOutOfRangeException(nameof(limit), limit, $"Spotify search limit must be between 1 and {MaxSearchLimit}.");
        }

        var parameters = new List<string>
        {
            $"q={Uri.EscapeDataString(query)}",
            $"type={Uri.EscapeDataString(type)}",
            $"limit={limit}"
        };

        if (!string.IsNullOrWhiteSpace(market))
        {
            parameters.Add($"market={Uri.EscapeDataString(market)}");
        }

        return $"search?{string.Join("&", parameters)}";
    }

    private static void ValidateIdentifier(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Spotify identifier must be provided.", paramName);
        }
    }
}
