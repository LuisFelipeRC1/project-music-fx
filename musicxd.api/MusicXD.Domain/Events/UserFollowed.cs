using MusicXD.Domain.Abstractions;

namespace MusicXD.Domain.Events;

public sealed record UserFollowed(
    Guid FollowId,
    Guid FollowerId,
    Guid FolloweeId,
    DateTime OccurredAt) : IDomainEvent;
