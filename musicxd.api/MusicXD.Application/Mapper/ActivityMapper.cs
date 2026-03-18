using MusicXD.Application.DTOs;
using MusicXD.Domain.Entities;
using MusicXD.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace MusicXD.Application.Mapper;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class ActivityMapper
{
    [MapProperty(nameof(Activity.Type), nameof(ActivityFeedDto.EventType))]
    public static partial ActivityFeedDto ToActivityFeedDto(this Activity activity);

    private static string Map(ActivityType type) => type.ToString();
}
