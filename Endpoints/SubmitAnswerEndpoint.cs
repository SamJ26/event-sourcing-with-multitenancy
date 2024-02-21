using EventSourcing.Events;
using EventSourcing.Persistence;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Endpoints;

public sealed class SubmitAnswerEndpoint
{
    public static IResult Handle(
        [FromRoute] int instanceId,
        [FromBody] SubmitAnswerRequest req,
        [FromServices] AppDbContext dbContext,
        [FromServices] IDocumentStore documentStore,
        [FromServices] TenantContextProvider tenantContextProvider,
        [FromServices] ILogger<SubmitAnswerEndpoint> logger,
        CancellationToken ct)
    {
        var tenant = tenantContextProvider.Current;
        
        var instance = dbContext
            .Instances
            .Find(instanceId);

        if (instance is null)
        {
            throw new Exception($"Instance with id '{instanceId}' does not exist!");
        }

        // TODO: create a session on database specified by tenant.Database
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

            var instanceEvent = new AnswerSubmittedEvent()
            {
                InstanceId = instanceId,
                Text = req.Text
            };

            session.Events.Append(instance.EventStreamId, instanceEvent);
            session.SaveChanges();
        }

        return Results.Ok();
    }
}

public record SubmitAnswerRequest(string Text);