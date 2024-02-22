namespace EventSourcing.Events;

public abstract class EventBase
{
    public required int GameId { get; init; }
}