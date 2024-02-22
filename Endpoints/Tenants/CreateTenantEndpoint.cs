using EventSourcing.Persistence;
using Marten;
using Marten.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Endpoints.Tenants;

public sealed class CreateTenantEndpoint
{
    public static async Task<IResult> Handle(
        [FromBody] CreateTenantRequest req,
        [FromServices] ConnectionStringFactory connectionStringFactory,
        [FromServices] IDocumentStore documentStore,
        CancellationToken ct)
    {
        var tenantDbContextConnectionString = connectionStringFactory.CreateForTenantDbContext(req.TenantIdentifier);

        // Create database for the new tenant
        await using (var dbContext = new TenantDbContext(tenantDbContextConnectionString))
        {
            await dbContext.Database.MigrateAsync(ct);
        }

        // Store tenant connection string
        var tenancy = (MasterTableTenancy)documentStore.Options.Tenancy;
        await tenancy.AddDatabaseRecordAsync(req.TenantIdentifier, tenantDbContextConnectionString);

        return Results.Ok();
    }
}

public record CreateTenantRequest(string TenantIdentifier);