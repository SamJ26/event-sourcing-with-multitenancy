using Microsoft.EntityFrameworkCore.Design;

namespace EventSourcing.Persistence;

public sealed class DesignTimeTenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
{
    public TenantDbContext CreateDbContext(string[] args)
    {
        return new TenantDbContext("ThisDbContextIsOnlyForMigrations");
    }
}