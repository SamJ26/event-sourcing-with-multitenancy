// using EventSourcing.Events;
// using EventSourcing.Persistence;
// using Marten;
// using Microsoft.AspNetCore.Mvc;
//
// namespace EventSourcing.Endpoints.Games;
//
// public sealed class TerminateInstanceEndpoint
// {
//     public static IResult Handle(
//         [FromRoute] int instanceId,
//         [FromServices] TenantDbContext dbContext,
//         [FromServices] IDocumentStore documentStore,
//         [FromServices] TenantContextProvider tenantContextProvider,
//         [FromServices] ILogger<TerminateInstanceEndpoint> logger,
//         CancellationToken ct)
//     {
//         var tenant = tenantContextProvider.Current;
//
//         var instance = dbContext
//             .Instances
//             .Find(instanceId);
//
//         if (instance is null)
//         {
//             throw new Exception($"Instance with id '{instanceId}' does not exist!");
//         }
//
//         // TODO: create a session on database specified by tenant.Database
//         using (var session = documentStore.LightweightSession())
//         {
//             var instanceAggregate = session
//                 .Events
//                 .AggregateStream<InstanceAggregate>(instance.EventStreamId)!;
//
//             logger.LogInformation("Instance state: {Aggregate}", instanceAggregate);
//
//             if (instanceAggregate.IsTerminated)
//             {
//                 throw new Exception($"Instance with id '{instanceId}' was terminated!");
//             }
//
//             var instanceEvent = new InstanceTerminatedEvent()
//             {
//                 InstanceId = instanceId
//             };
//
//             session.Events.Append(instance.EventStreamId, instanceEvent);
//             session.SaveChanges();
//         }
//
//         return Results.Ok();
//     }
// }