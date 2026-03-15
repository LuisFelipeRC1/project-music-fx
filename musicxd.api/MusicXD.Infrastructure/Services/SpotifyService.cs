using System.Net.Http.Json;
using MusicXD.Application.DTOs;
using MusicXD.Application.Interfaces;

namespace MusicXD.Infrastructure.Services;

public class SpotifyService : ISpotifyService
{
    private readonly HttpClient _httpClient;

    public SpotifyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/");
    }

    public async Task<IEnumerable<TrackDto>> GetTopTracksAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Get, "me/top/tracks?limit=20", accessToken);
        using var httpResponse = await _httpClient.SendAsync(request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();
        var response = await httpResponse.Content.ReadFromJsonAsync<SpotifyTopTracksResponse>(cancellationToken: cancellationToken);
        return response?.Items?.Select(MapToTrackDto) ?? Enumerable.Empty<TrackDto>();
    }

    public async Task<IEnumerable<TrackDto>> GetRecentlyPlayedAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Get, "me/player/recently-played?limit=20", accessToken);
        using var httpResponse = await _httpClient.SendAsync(request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();
        var response = await httpResponse.Content.ReadFromJsonAsync<SpotifyRecentlyPlayedResponse>(cancellationToken: cancellationToken);
        return response?.Items?.Select(i => MapToTrackDto(i.Track)) ?? Enumerable.Empty<TrackDto>();
    }

    public async Task<IEnumerable<ArtistDto>> GetTopArtistsAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        using var request = CreateRequest(HttpMethod.Get, "me/top/artists?limit=20", accessToken);
        using var httpResponse = await _httpClient.SendAsync(request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();
        var response = await httpResponse.Content.ReadFromJsonAsync<SpotifyTopArtistsResponse>(cancellationToken: cancellationToken);
        return response?.Items?.Select(MapToArtistDto) ?? Enumerable.Empty<ArtistDto>();
    }

    private static HttpRequestMessage CreateRequest(HttpMethod method, string relativeUrl, string accessToken)
    {
        var request = new HttpRequestMessage(method, relativeUrl);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        return request;
    }

    private static TrackDto MapToTrackDto(SpotifyTrack track) => new()
    {
        // Id is intentionally empty — these are transient Spotify results, not persisted DB entities
        Id = Guid.Empty,
        SpotifyId = track.Id ?? string.Empty,
        Title = track.Name ?? string.Empty,
        DurationMs = track.DurationMs
    };

    private static ArtistDto MapToArtistDto(SpotifyArtist artist) => new()
    {
        // Id is intentionally empty — these are transient Spotify results, not persisted DB entities
        Id = Guid.Empty,
        SpotifyId = artist.Id ?? string.Empty,
        Name = artist.Name ?? string.Empty,
        Genres = artist.Genres ?? new List<string>()
    };

    private record SpotifyTopTracksResponse(List<SpotifyTrack>? Items);
    private record SpotifyRecentlyPlayedResponse(List<SpotifyRecentItem>? Items);
    private record SpotifyRecentItem(SpotifyTrack Track);
    private record SpotifyTopArtistsResponse(List<SpotifyArtist>? Items);
    private record SpotifyTrack(string? Id, string? Name, int DurationMs);
    private record SpotifyArtist(string? Id, string? Name, List<string>? Genres);
}
