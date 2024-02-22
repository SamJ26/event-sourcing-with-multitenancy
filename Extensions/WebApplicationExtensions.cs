using EventSourcing.Endpoints.Tenants;

namespace EventSourcing.Extensions;

public static class WebApplicationExtensions
{
    public static void UseGamesModule(this WebApplication app)
    {
        var group = app
            .MapGroup("games")
            .WithTags("Games");

        // group.MapPost(string.Empty, StartInstanceEndpoint.Handle);
        // group.MapPut("{instanceId:int}/submit-answer", SubmitAnswerEndpoint.Handle);
        // group.MapPut("{instanceId:int}/terminate", TerminateInstanceEndpoint.Handle);
    }

    public static void UseTenantsModule(this WebApplication app)
    {
        var group = app
            .MapGroup("tenants")
            .WithTags("Tenants");

        group.MapPost(string.Empty, CreateTenantEndpoint.Handle);
    }
}