using System.Globalization;
using MusicXD.Application.DTOs;
using MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyModels;

namespace MusicXD.Infrastructure.ExternalServices.Spotify.SpotifyMapper;

public static class SpotifyMapper
{
    public static ArtistDto ToArtistDto(this SpotifyArtistResponse artist) => new()
    {
        Id = Guid.Empty,
        SpotifyId = artist.Id,
        Name = artist.Name,
        ImageUrl = artist.Images.FirstOrDefault()?.Url,
        Genres = artist.Genres
    };

    public static AlbumDto ToAlbumDto(this SpotifyAlbumResponse album)
    {
        var primaryArtist = album.Artists.FirstOrDefault();

        return new AlbumDto
        {
            Id = Guid.Empty,
            SpotifyId = album.Id,
            Title = album.Name,
            ArtistId = Guid.Empty,
            ArtistName = primaryArtist?.Name ?? string.Empty,
            ReleaseDate = ParseReleaseDate(album.ReleaseDate, album.ReleaseDatePrecision),
            CoverImageUrl = album.Images.FirstOrDefault()?.Url,
            Genres = album.Genres
        };
    }

    public static TrackDto ToTrackDto(this SpotifyTrackResponse track) => new()
    {
        Id = Guid.Empty,
        SpotifyId = track.Id,
        Title = track.Name,
        AlbumId = Guid.Empty,
        AlbumTitle = track.Album?.Name ?? string.Empty,
        DurationMs = track.DurationMs,
        TrackNumber = track.TrackNumber
    };

    private static DateTime ParseReleaseDate(string? releaseDate, string? precision)
    {
        if (string.IsNullOrWhiteSpace(releaseDate))
        {
            return DateTime.MinValue;
        }

        var formats = precision switch
        {
            "year" => new[] { "yyyy" },
            "month" => new[] { "yyyy-MM" },
            _ => new[] { "yyyy-MM-dd", "yyyy-MM", "yyyy" }
        };

        return DateTime.TryParseExact(
            releaseDate,
            formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
            out var parsedDate)
            ? parsedDate
            : DateTime.MinValue;
    }
}
