using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicXD.API.Mapper;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyClient;

namespace MusicXD.API.Controllers;

[ApiController]
[Route("api/spotify/catalog")]
[AllowAnonymous]
public class SpotifyCatalogController : ControllerBase
{
    private readonly ISpotifyClient _spotifyClient;

    public SpotifyCatalogController(ISpotifyClient spotifyClient)
    {
        _spotifyClient = spotifyClient;
    }

    [HttpGet("artists/{id}")]
    public async Task<IActionResult> GetArtist(string id, CancellationToken cancellationToken)
    {
        var artist = await _spotifyClient.GetArtistAsync(id, cancellationToken);
        return Ok(artist.ToSpotifyArtistDto());
    }

    [HttpGet("albums/{id}")]
    public async Task<IActionResult> GetAlbum(string id, [FromQuery] string? market, CancellationToken cancellationToken)
    {
        var album = await _spotifyClient.GetAlbumAsync(id, market, cancellationToken);
        return Ok(album.ToSpotifyAlbumDto());
    }

    [HttpGet("tracks/{id}")]
    public async Task<IActionResult> GetTrack(string id, [FromQuery] string? market, CancellationToken cancellationToken)
    {
        var track = await _spotifyClient.GetTrackAsync(id, market, cancellationToken);
        return Ok(track.ToSpotifyTrackDto());
    }

    [HttpGet("search/artists")]
    public async Task<IActionResult> SearchArtists([FromQuery(Name = "q")] string query, [FromQuery] int limit = 10, [FromQuery] string? market = null, CancellationToken cancellationToken = default)
    {
        var artists = await _spotifyClient.SearchArtistsAsync(query, limit, market, cancellationToken);
        return Ok(artists.ToSpotifyArtistSearchResultDto());
    }

    [HttpGet("search/albums")]
    public async Task<IActionResult> SearchAlbums([FromQuery(Name = "q")] string query, [FromQuery] int limit = 10, [FromQuery] string? market = null, CancellationToken cancellationToken = default)
    {
        var albums = await _spotifyClient.SearchAlbumsAsync(query, limit, market, cancellationToken);
        return Ok(albums.ToSpotifyAlbumSearchResultDto());
    }

    [HttpGet("search/tracks")]
    public async Task<IActionResult> SearchTracks([FromQuery(Name = "q")] string query, [FromQuery] int limit = 10, [FromQuery] string? market = null, CancellationToken cancellationToken = default)
    {
        var tracks = await _spotifyClient.SearchTracksAsync(query, limit, market, cancellationToken);
        return Ok(tracks.ToSpotifyTrackSearchResultDto());
    }
}
