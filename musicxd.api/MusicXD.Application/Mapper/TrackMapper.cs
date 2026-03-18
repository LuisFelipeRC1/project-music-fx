using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace MusicXD.Application.Mapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class TrackMapper
{
    [MapperIgnoreTarget(nameof(TrackDto.AlbumTitle))]
    public static partial TrackDto ToTrackDto(this Track track);

    public static partial TrackDto ToTrackDto(this Track track, string albumTitle);

    private static string Map(SpotifyId spotifyId) => spotifyId.Value;
}
