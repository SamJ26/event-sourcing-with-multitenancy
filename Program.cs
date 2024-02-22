using EventSourcing.Extensions;
using EventSourcing.MultiTenancy;
using EventSourcing.Persistence;
using EventSourcing.Persistence.Options;
using Marten;
using Weasel.Core;

namespace EventSourcing;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddTransient<MultiTenancyMiddleware>();
            services.AddScoped<TenantContextProvider>();

            services.AddPersistence(configuration);

            ConfigureMarten(services, configuration);
        }

        var app = builder.Build();
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<MultiTenancyMiddleware>();

            app.UseTenantsModule();
            app.UseGamesModule();
        }

        app.Run();
    }

    private static void ConfigureMarten(IServiceCollection services, IConfiguration configuration)
    {
        var databaseOptions = configuration
            .GetSection("Persistence:DatabaseOptions")
            .Get<DatabaseOptions>()!;

        var connectionStringOptions = configuration
            .GetSection("Persistence:ConnectionStringOptions")
            .Get<ConnectionStringOptions>()!;

        var connectionString = $"Host={connectionStringOptions.Host};" +
                               $"Port={connectionStringOptions.Port};" +
                               $"Database={databaseOptions.MasterDatabaseName};" +
                               $"Username={connectionStringOptions.Username};" +
                               $"Password={connectionStringOptions.Password}";

        services
            .AddMarten((options) =>
            {
                options.MultiTenantedDatabasesWithMasterDatabaseTable(connectionString);
                options.AutoCreateSchemaObjects = AutoCreate.All;
            })
            .ApplyAllDatabaseChangesOnStartup();
    }
}