using EventSourcing.Events;

namespace EventSourcing;

public sealed class InstanceAggregate
{
    public int Id { get; private set; }
    public bool IsTerminated { get; private set; }
    public string Answer { get; private set; } = null!;

    public void Apply(InstanceStartedEvent e)
    {
        Id = e.InstanceId;
    }

    public void Apply(AnswerSubmittedEvent e)
    {
        Answer = e.Text;
    }

    public void Apply(InstanceTerminatedEvent _)
    {
        IsTerminated = true;
    }

    public override string ToString()
    {
        return $"{{ Id: {Id}, IsTerminated: {IsTerminated}, Answer: {Answer} }}";
    }
}