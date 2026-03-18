using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace MusicXD.Application.Mapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class ArtistMapper
{
    public static partial ArtistDto ToArtistDto(this Artist artist);

    private static string Map(SpotifyId spotifyId) => spotifyId.Value;
}
