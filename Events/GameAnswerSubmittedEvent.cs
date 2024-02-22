namespace EventSourcing.Events;

public sealed class GameAnswerSubmittedEvent : EventBase
{
    public required string Text { get; init; }
}