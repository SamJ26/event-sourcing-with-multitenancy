using EventSourcing.Persistence.Options;
using Microsoft.Extensions.Options;

namespace EventSourcing.Persistence;

public sealed class ConnectionStringFactory(
    IOptions<ConnectionStringOptions> connectionStringOptions,
    IOptions<DatabaseOptions> databaseOptions)
{
    private readonly ConnectionStringOptions _connectionStringOptions = connectionStringOptions.Value;
    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;

    public string CreateForTenantDbContext(string tenantIdentifier)
    {
        return $"Host={_connectionStringOptions.Host};" +
               $"Port={_connectionStringOptions.Port};" +
               $"Database={_databaseOptions.TenantDatabasePrefix + tenantIdentifier};" +
               $"Username={_connectionStringOptions.Username};" +
               $"Password={_connectionStringOptions.Password}";
    }

    public string CreateForMasterDbContext()
    {
        return $"Host={_connectionStringOptions.Host};" +
               $"Port={_connectionStringOptions.Port};" +
               $"Database={_databaseOptions.MasterDatabaseName};" +
               $"Username={_connectionStringOptions.Username};" +
               $"Password={_connectionStringOptions.Password}";
    }
}