using EventSourcing.Persistence.Options;

namespace EventSourcing.Persistence;

public static class ConfigureServices
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ConnectionStringFactory>();

        services.AddOptions<ConnectionStringOptions>()
            .Bind(configuration.GetSection("Persistence:ConnectionStringOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection("Persistence:DatabaseOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}