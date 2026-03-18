using MusicXD.Domain.Abstractions;

namespace MusicXD.Domain.Events;

public sealed record TrackRated(
    Guid RatingId,
    Guid UserId,
    Guid TrackId,
    decimal Rating,
    DateTime OccurredAt) : IDomainEvent;
