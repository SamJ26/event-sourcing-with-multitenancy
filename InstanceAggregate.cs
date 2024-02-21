using EventSourcing.Events;

namespace EventSourcing;

public record InstanceAggregate
{
    public int Id { get; set; }
    public bool IsTerminated { get; set; }
    public string Answer { get; set; } = null!;

    public void Apply(InstanceStartedEvent e) => Id = e.InstanceId;

    public void Apply(AnswerSubmittedEvent e) => Answer = e.Text;

    public void Apply(InstanceTerminatedEvent e) => IsTerminated = true;
}