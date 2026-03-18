using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyConfiguration;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyAuthService;

public sealed class SpotifyAuthService : ISpotifyAuthService
{
    public const string HttpClientName = "SpotifyAuth";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly SpotifyOptions _options;
    private readonly SemaphoreSlim _tokenLock = new(1, 1);

    private string? _accessToken;
    private DateTimeOffset _accessTokenExpiresAt = DateTimeOffset.MinValue;

    public SpotifyAuthService(IHttpClientFactory httpClientFactory, IOptions<SpotifyOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (HasValidAccessToken())
        {
            return _accessToken!;
        }

        await _tokenLock.WaitAsync(cancellationToken);

        try
        {
            if (HasValidAccessToken())
            {
                return _accessToken!;
            }

            ValidateOptions();

            using var request = new HttpRequestMessage(HttpMethod.Post, "api/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", BuildBasicCredentials());
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            });

            using var response = await _httpClientFactory
                .CreateClient(HttpClientName)
                .SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<SpotifyTokenResponse>(SerializerOptions, cancellationToken)
                ?? throw new InvalidOperationException("Spotify token response was empty.");

            if (string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
            {
                throw new InvalidOperationException("Spotify token response did not contain an access token.");
            }

            _accessToken = tokenResponse.AccessToken;
            _accessTokenExpiresAt = DateTimeOffset.UtcNow.AddSeconds(GetRefreshWindow(tokenResponse.ExpiresIn));

            return _accessToken;
        }
        finally
        {
            _tokenLock.Release();
        }
    }

    private bool HasValidAccessToken() =>
        !string.IsNullOrWhiteSpace(_accessToken) && _accessTokenExpiresAt > DateTimeOffset.UtcNow;

    private string BuildBasicCredentials()
    {
        var credentials = $"{_options.ClientId}:{_options.ClientSecret}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
    }

    private void ValidateOptions()
    {
        if (string.IsNullOrWhiteSpace(_options.ClientId))
        {
            throw new InvalidOperationException("Spotify ClientId is not configured.");
        }

        if (string.IsNullOrWhiteSpace(_options.ClientSecret))
        {
            throw new InvalidOperationException("Spotify ClientSecret is not configured.");
        }
    }

    private static int GetRefreshWindow(int expiresInSeconds)
    {
        if (expiresInSeconds <= 10)
        {
            return expiresInSeconds;
        }

        return Math.Max(1, expiresInSeconds - 30);
    }
}
