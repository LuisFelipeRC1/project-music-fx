using MusicXD.Domain.Abstractions;

namespace MusicXD.Domain.Events;

public sealed record AlbumReviewed(
    Guid ReviewId,
    Guid UserId,
    Guid AlbumId,
    decimal Rating,
    DateTime OccurredAt) : IDomainEvent;
