using MusicXD.Domain.Abstractions;

namespace MusicXD.Domain.Events;

public sealed record TrackRated(
    Guid ReviewId,
    Guid UserId,
    Guid TrackId,
    decimal Rating,
    DateTime OccurredAt) : IDomainEvent;
