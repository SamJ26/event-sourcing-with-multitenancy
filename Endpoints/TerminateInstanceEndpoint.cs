using EventSourcing.Events;
using EventSourcing.Persistence;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Endpoints;

public sealed class TerminateInstanceEndpoint
{
    public static IResult Handle(
        [FromRoute] int instanceId,
        [FromServices] AppDbContext dbContext,
        [FromServices] IDocumentStore documentStore,
        [FromServices] ILogger<TerminateInstanceEndpoint> logger,
        CancellationToken ct)
    {
        var instance = dbContext
            .Instances
            .Find(instanceId);

        if (instance is null)
        {
            throw new Exception($"Instance with id '{instanceId}' does not exist!");
        }

        using (var session = documentStore.LightweightSession())
        {
            var instanceAggregate = session
                .Events
                .AggregateStream<InstanceAggregate>(instance.EventStreamId)!;

            logger.LogInformation("Instance state: {Aggregate}", instanceAggregate);

            if (instanceAggregate.IsTerminated)
            {
                throw new Exception($"Instance with id '{instanceId}' was terminated!");
            }

            var instanceEvent = new InstanceTerminatedEvent()
            {
                InstanceId = instanceId
            };

            session.Events.Append(instance.EventStreamId, instanceEvent);
            session.SaveChanges();
        }

        return Results.Ok();
    }
}