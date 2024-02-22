using EventSourcing.Events;
using EventSourcing.MultiTenancy;
using EventSourcing.Persistence;
using EventSourcing.Persistence.Entities;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Endpoints.Games;

public sealed class StartGameEndpoint
{
    public static async Task<IResult> Handle(
        [FromHeader] string tenantIdentifier,
        [FromServices] IDocumentStore documentStore,
        [FromServices] TenantContextProvider tenantContextProvider,
        [FromServices] ConnectionStringFactory connectionStringFactory,
        CancellationToken ct)
    {
        var tenant = tenantContextProvider.Current!;

        var game = new GameEntity()
        {
            EventStreamId = Guid.NewGuid()
        };

        // TODO: make sure that both operations (save game and save event) will complete succesfully

        await using (var dbContext = new TenantDbContext(connectionStringFactory.CreateForTenantDbContext(tenant.Identifier)))
        {
            dbContext.Add(game);
            await dbContext.SaveChangesAsync(ct);
        }

        await using (var session = documentStore.LightweightSession(tenant.Identifier))
        {
            var gameEvent = new GameStartedEvent()
            {
                GameId = game.Id
            };

            session.Events.Append(game.EventStreamId, gameEvent);
            await session.SaveChangesAsync(ct);
        }

        return Results.Ok(game.Id);
    }
}