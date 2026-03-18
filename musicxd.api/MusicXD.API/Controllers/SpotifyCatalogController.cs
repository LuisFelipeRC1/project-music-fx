using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyClient;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/spotify/catalog")]
[AllowAnonymous]
public class SpotifyCatalogController : ControllerBase
{
    private readonly ISpotifyClient _spotifyClient;

    // DTOs used to avoid exposing Infrastructure/Spotify models directly
    public sealed record SpotifyArtistDto(object Artist);
    public sealed record SpotifyAlbumDto(object Album);
    public sealed record SpotifyTrackDto(object Track);
    public sealed record SpotifyArtistSearchResultDto(object Result);
    public sealed record SpotifyAlbumSearchResultDto(object Result);
    public sealed record SpotifyTrackSearchResultDto(object Result);

    public SpotifyCatalogController(ISpotifyClient spotifyClient)
    {
        _spotifyClient = spotifyClient;
    }

    [HttpGet("artists/{id}")]
    public async Task<IActionResult> GetArtist(string id, CancellationToken cancellationToken)
    {
        var artist = await _spotifyClient.GetArtistAsync(id, cancellationToken);
        return Ok(new SpotifyArtistDto(artist));
    }

    [HttpGet("albums/{id}")]
    public async Task<IActionResult> GetAlbum(string id, [FromQuery] string? market, CancellationToken cancellationToken)
    {
        var album = await _spotifyClient.GetAlbumAsync(id, market, cancellationToken);
        return Ok(new SpotifyAlbumDto(album));
    }

    [HttpGet("tracks/{id}")]
    public async Task<IActionResult> GetTrack(string id, [FromQuery] string? market, CancellationToken cancellationToken)
    {
        var track = await _spotifyClient.GetTrackAsync(id, market, cancellationToken);
        return Ok(new SpotifyTrackDto(track));
    }

    [HttpGet("search/artists")]
    public async Task<IActionResult> SearchArtists([FromQuery(Name = "q")] string query, [FromQuery] int limit = 10, [FromQuery] string? market = null, CancellationToken cancellationToken = default)
    {
        var artists = await _spotifyClient.SearchArtistsAsync(query, limit, market, cancellationToken);
        return Ok(new SpotifyArtistSearchResultDto(artists));
    }

    [HttpGet("search/albums")]
    public async Task<IActionResult> SearchAlbums([FromQuery(Name = "q")] string query, [FromQuery] int limit = 10, [FromQuery] string? market = null, CancellationToken cancellationToken = default)
    {
        var albums = await _spotifyClient.SearchAlbumsAsync(query, limit, market, cancellationToken);
        return Ok(new SpotifyAlbumSearchResultDto(albums));
    }

    [HttpGet("search/tracks")]
    public async Task<IActionResult> SearchTracks([FromQuery(Name = "q")] string query, [FromQuery] int limit = 10, [FromQuery] string? market = null, CancellationToken cancellationToken = default)
    {
        var tracks = await _spotifyClient.SearchTracksAsync(query, limit, market, cancellationToken);
        return Ok(new SpotifyTrackSearchResultDto(tracks));
    }
}
