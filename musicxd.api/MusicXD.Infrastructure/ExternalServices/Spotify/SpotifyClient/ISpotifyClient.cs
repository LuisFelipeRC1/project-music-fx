using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyClient;

public interface ISpotifyClient
{
    Task<SpotifyArtistResponse> GetArtistAsync(string artistId, CancellationToken cancellationToken = default);
    Task<SpotifyAlbumResponse> GetAlbumAsync(string albumId, string? market = null, CancellationToken cancellationToken = default);
    Task<SpotifyTrackResponse> GetTrackAsync(string trackId, string? market = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SpotifyArtistResponse>> SearchArtistsAsync(string query, int limit = 10, string? market = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SpotifyAlbumResponse>> SearchAlbumsAsync(string query, int limit = 10, string? market = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SpotifyTrackResponse>> SearchTracksAsync(string query, int limit = 10, string? market = null, CancellationToken cancellationToken = default);
}
