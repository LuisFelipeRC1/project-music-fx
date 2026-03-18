using MusicXD.Domain.Abstractions;

namespace MusicXD.Domain.Events;

public sealed record UserRegistered(
    Guid UserId,
    DateTime OccurredAt) : IDomainEvent;
