namespace EventSourcing.Events;

public sealed class AnswerSubmittedEvent : EventBase
{
    public required string Text { get; init; }
}