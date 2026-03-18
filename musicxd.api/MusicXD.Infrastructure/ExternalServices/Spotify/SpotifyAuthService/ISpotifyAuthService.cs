namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyAuthService;

public interface ISpotifyAuthService
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
