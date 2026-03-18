using MusicXD.API.Contracts;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;
using Riok.Mapperly.Abstractions;

namespace MusicXD.API.Mapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class SpotifyCatalogMapper
{
    public static partial SpotifyImageDto ToSpotifyImageDto(this SpotifyImage image);

    public static partial SpotifyArtistSummaryDto ToSpotifyArtistSummaryDto(this SpotifyArtistSummary artist);

    public static partial SpotifyAlbumSummaryDto ToSpotifyAlbumSummaryDto(this SpotifyAlbumSummary album);

    public static partial SpotifyArtistDetailsDto ToSpotifyArtistDetailsDto(this SpotifyArtistResponse artist);

    public static partial SpotifyAlbumDetailsDto ToSpotifyAlbumDetailsDto(this SpotifyAlbumResponse album);

    public static partial SpotifyTrackDetailsDto ToSpotifyTrackDetailsDto(this SpotifyTrackResponse track);

    public static partial List<SpotifyArtistDetailsDto> ToSpotifyArtistDetailsDtos(this IReadOnlyList<SpotifyArtistResponse> artists);

    public static partial List<SpotifyAlbumDetailsDto> ToSpotifyAlbumDetailsDtos(this IReadOnlyList<SpotifyAlbumResponse> albums);

    public static partial List<SpotifyTrackDetailsDto> ToSpotifyTrackDetailsDtos(this IReadOnlyList<SpotifyTrackResponse> tracks);

    public static SpotifyArtistDto ToSpotifyArtistDto(this SpotifyArtistResponse artist) => new()
    {
        Artist = artist.ToSpotifyArtistDetailsDto()
    };

    public static SpotifyAlbumDto ToSpotifyAlbumDto(this SpotifyAlbumResponse album) => new()
    {
        Album = album.ToSpotifyAlbumDetailsDto()
    };

    public static SpotifyTrackDto ToSpotifyTrackDto(this SpotifyTrackResponse track) => new()
    {
        Track = track.ToSpotifyTrackDetailsDto()
    };

    public static SpotifyArtistSearchResultDto ToSpotifyArtistSearchResultDto(this IReadOnlyList<SpotifyArtistResponse> artists) => new()
    {
        Result = artists.ToSpotifyArtistDetailsDtos()
    };

    public static SpotifyAlbumSearchResultDto ToSpotifyAlbumSearchResultDto(this IReadOnlyList<SpotifyAlbumResponse> albums) => new()
    {
        Result = albums.ToSpotifyAlbumDetailsDtos()
    };

    public static SpotifyTrackSearchResultDto ToSpotifyTrackSearchResultDto(this IReadOnlyList<SpotifyTrackResponse> tracks) => new()
    {
        Result = tracks.ToSpotifyTrackDetailsDtos()
    };
}
