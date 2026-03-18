using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace MusicXD.Application.Mapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class AlbumReviewMapper
{
    [MapperIgnoreTarget(nameof(AlbumReviewDto.Username))]
    [MapProperty(nameof(AlbumReview.ReviewText), nameof(AlbumReviewDto.Content))]
    public static partial AlbumReviewDto ToAlbumReviewDto(this AlbumReview review);

    [MapProperty(nameof(AlbumReview.ReviewText), nameof(AlbumReviewDto.Content))]
    public static partial AlbumReviewDto ToAlbumReviewDto(this AlbumReview review, string username);

    private static decimal Map(Rating rating) => rating.Value;

    private static string Map(ReviewText reviewText) => reviewText.Value;
}
