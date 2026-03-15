namespace MusicXD.Domain.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredAt { get; }
}
