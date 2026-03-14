using MusicXD.Application.DTOs;

namespace MusicXD.Application.Interfaces;

public interface ISpotifyService
{
    Task<IEnumerable<TrackDto>> GetTopTracksAsync(string accessToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<TrackDto>> GetRecentlyPlayedAsync(string accessToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<ArtistDto>> GetTopArtistsAsync(string accessToken, CancellationToken cancellationToken = default);
}
