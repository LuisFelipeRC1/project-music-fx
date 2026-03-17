namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyConfiguration;

public class SpotifyOptions
{
    public string BaseUrl { get; set; } = "https://api.spotify.com/v1";
    public string AuthUrl { get; set; } = "https://accounts.spotify.com";
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;
}