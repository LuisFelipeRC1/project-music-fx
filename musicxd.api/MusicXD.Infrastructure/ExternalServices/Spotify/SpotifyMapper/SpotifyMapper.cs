using System.Globalization;
using MusicXD.Application.DTOs;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;
using Riok.Mapperly.Abstractions;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyMapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class SpotifyMapper
{
    [MapperIgnoreTarget(nameof(ArtistDto.Id))]
    [MapProperty(nameof(SpotifyArtistResponse.Id), nameof(ArtistDto.SpotifyId))]
    [MapProperty(nameof(SpotifyArtistResponse.Images), nameof(ArtistDto.ImageUrl))]
    public static partial ArtistDto ToArtistDto(this SpotifyArtistResponse artist);

    [MapperIgnoreTarget(nameof(AlbumDto.Id))]
    [MapperIgnoreTarget(nameof(AlbumDto.ArtistId))]
    [MapProperty(nameof(SpotifyAlbumResponse.Id), nameof(AlbumDto.SpotifyId))]
    [MapProperty(nameof(SpotifyAlbumResponse.Name), nameof(AlbumDto.Title))]
    [MapProperty(nameof(SpotifyAlbumResponse.ReleaseDate), nameof(AlbumDto.ReleaseDate))]
    [MapProperty(nameof(SpotifyAlbumResponse.Images), nameof(AlbumDto.CoverImageUrl))]
    [MapProperty(nameof(SpotifyAlbumResponse.Artists), nameof(AlbumDto.ArtistName))]
    public static partial AlbumDto ToAlbumDto(this SpotifyAlbumResponse album);

    [MapperIgnoreTarget(nameof(TrackDto.Id))]
    [MapperIgnoreTarget(nameof(TrackDto.AlbumId))]
    [MapProperty(nameof(SpotifyTrackResponse.Id), nameof(TrackDto.SpotifyId))]
    [MapProperty(nameof(SpotifyTrackResponse.Name), nameof(TrackDto.Title))]
    [MapProperty(nameof(SpotifyTrackResponse.Album), nameof(TrackDto.AlbumTitle))]
    public static partial TrackDto ToTrackDto(this SpotifyTrackResponse track);

    private static string? Map(List<SpotifyImage> images) => images.FirstOrDefault()?.Url;

    private static string Map(List<SpotifyArtistSummary> artists) => artists.FirstOrDefault()?.Name ?? string.Empty;

    private static string Map(SpotifyAlbumSummary? album) => album?.Name ?? string.Empty;

    private static DateTime Map(string? releaseDate)
    {
        if (string.IsNullOrWhiteSpace(releaseDate))
        {
            return DateTime.MinValue;
        }

        return TryParseReleaseDate(releaseDate, out var parsedDate)
            ? parsedDate
            : DateTime.MinValue;
    }

    private static bool TryParseReleaseDate(string releaseDate, out DateTime parsedDate)
    {
        return DateTime.TryParseExact(
            releaseDate,
            new[] { "yyyy-MM-dd", "yyyy-MM", "yyyy" },
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
            out parsedDate);
    }
}
