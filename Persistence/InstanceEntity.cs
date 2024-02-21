namespace EventSourcing.Persistence;

public sealed class InstanceEntity
{
    public int Id { get; init; }
    public required string Name { get; init; }
}