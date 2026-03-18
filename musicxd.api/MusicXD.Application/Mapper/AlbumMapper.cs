using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace MusicXD.Application.Mapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class AlbumMapper
{
    [MapperIgnoreTarget(nameof(AlbumDto.ArtistName))]
    public static partial AlbumDto ToAlbumDto(this Album album);

    public static partial AlbumDto ToAlbumDto(this Album album, string artistName);

    private static string Map(SpotifyId spotifyId) => spotifyId.Value;
}
