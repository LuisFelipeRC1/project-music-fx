using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Application.Interfaces;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/spotify")]
[Authorize]
public class SpotifyController : ControllerBase
{
    private readonly ISpotifyService _spotifyService;

    public SpotifyController(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    [HttpGet("top-tracks")]
    public async Task<IActionResult> GetTopTracks(CancellationToken cancellationToken)
    {
        var accessToken = GetSpotifyAccessToken();
        var result = await _spotifyService.GetTopTracksAsync(accessToken, cancellationToken);
        return Ok(result);
    }

    [HttpGet("recently-played")]
    public async Task<IActionResult> GetRecentlyPlayed(CancellationToken cancellationToken)
    {
        var accessToken = GetSpotifyAccessToken();
        var result = await _spotifyService.GetRecentlyPlayedAsync(accessToken, cancellationToken);
        return Ok(result);
    }

    [HttpGet("top-artists")]
    public async Task<IActionResult> GetTopArtists(CancellationToken cancellationToken)
    {
        var accessToken = GetSpotifyAccessToken();
        var result = await _spotifyService.GetTopArtistsAsync(accessToken, cancellationToken);
        return Ok(result);
    }

    private string GetSpotifyAccessToken()
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        return string.IsNullOrEmpty(token)
            ? throw new UnauthorizedAccessException("No access token provided.")
            : token;
    }
}
