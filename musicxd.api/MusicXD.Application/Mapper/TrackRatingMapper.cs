using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace MusicXD.Application.Mapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class TrackRatingMapper
{
    [MapperIgnoreTarget(nameof(TrackRatingDto.Username))]
    public static partial TrackRatingDto ToTrackRatingDto(this TrackRating rating);

    public static partial TrackRatingDto ToTrackRatingDto(this TrackRating rating, string username);

    private static decimal Map(Rating rating) => rating.Value;
}
