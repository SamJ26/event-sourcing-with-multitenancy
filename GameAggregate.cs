using EventSourcing.Events;

namespace EventSourcing;

public sealed class GameAggregate
{
    public int Id { get; private set; }
    public bool IsTerminated { get; private set; }
    public string Answer { get; private set; } = null!;

    public void Apply(GameStartedEvent e)
    {
        Id = e.GameId;
    }

    public void Apply(GameAnswerSubmittedEvent e)
    {
        Answer = e.Text;
    }

    public void Apply(GameTerminatedEvent _)
    {
        IsTerminated = true;
    }

    public override string ToString()
    {
        return $"{{ Id: {Id}, IsTerminated: {IsTerminated}, Answer: {Answer} }}";
    }
}