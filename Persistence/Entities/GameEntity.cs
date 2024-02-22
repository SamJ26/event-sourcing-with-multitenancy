namespace EventSourcing.Persistence.Entities;

public sealed class GameEntity
{
    public int Id { get; init; }
    public required Guid EventStreamId { get; init; }
}