namespace EventSourcing.Events;

public abstract class EventBase
{
    public required int InstanceId { get; init; }
}