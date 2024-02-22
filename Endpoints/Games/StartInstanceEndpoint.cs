// using EventSourcing.Events;
// using EventSourcing.Persistence;
// using EventSourcing.Persistence.Entities;
// using Marten;
// using Microsoft.AspNetCore.Mvc;
//
// namespace EventSourcing.Endpoints.Games;
//
// public sealed class StartInstanceEndpoint
// {
//     public static IResult Handle(
//         [FromServices] TenantDbContext dbContext,
//         [FromServices] IDocumentStore documentStore,
//         [FromServices] TenantContextProvider tenantContextProvider,
//         CancellationToken ct)
//     {
//         var tenant = tenantContextProvider.Current;
//
//         var instance = new GameEntity()
//         {
//             EventStreamId = Guid.NewGuid()
//         };
//
//         // TODO: make sure that both operations (save instance and save event) will complete succesfully
//
//         dbContext.Add(instance);
//         dbContext.SaveChanges();
//
//         // TODO: create a session on database specified by tenant.Database
//         using (var session = documentStore.LightweightSession("tenants"))
//         {
//             var instanceEvent = new InstanceStartedEvent()
//             {
//                 InstanceId = instance.Id
//             };
//
//             session.Events.Append(instance.EventStreamId, instanceEvent);
//             session.SaveChanges();
//         }
//
//         return Results.Ok(instance.Id);
//     }
// }