using EventSourcing.Events;
using EventSourcing.Persistence;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Endpoints;

public sealed class StartInstanceEndpoint
{
    public static IResult Handle(
        [FromServices] AppDbContext dbContext,
        [FromServices] IDocumentStore documentStore,
        CancellationToken ct)
    {
        var instance = new InstanceEntity()
        {
            EventStreamId = Guid.NewGuid()
        };

        // TODO: make sure that both operations (save instance and save event) will complete succesfully

        dbContext.Add(instance);
        dbContext.SaveChanges();

        using (var session = documentStore.LightweightSession())
        {
            var instanceEvent = new InstanceStartedEvent()
            {
                InstanceId = instance.Id
            };

            session.Events.Append(instance.EventStreamId, instanceEvent);
            session.SaveChanges();
        }

        return Results.Ok(instance.Id);
    }
}