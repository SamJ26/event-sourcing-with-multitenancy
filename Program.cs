using EventSourcing.Endpoints;
using EventSourcing.Persistence;
using Marten;
using Marten.Storage;
using Microsoft.EntityFrameworkCore;
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

            services.AddScoped<TenantContextProvider>();

            var connectionString = configuration.GetConnectionString("Default")!;

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            services.AddMarten((options) =>
            {
                options.Connection(connectionString);

                // If we're running in development mode, let Marten just take care of all necessary schema building and patching behind the scenes
                if (builder.Environment.IsDevelopment())
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }

                // TODO: is this correct configuration?
                options.Events.TenancyStyle = TenancyStyle.Conjoined;
            });
        }

        var app = builder.Build();
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var group = app
                .MapGroup("instances")
                .WithTags("Instances");

            group.MapPost(string.Empty, StartInstanceEndpoint.Handle);

            group.MapPut("{instanceId:int}/submit-answer", SubmitAnswerEndpoint.Handle);

            group.MapPut("{instanceId:int}/terminate", TerminateInstanceEndpoint.Handle);
        }

        app.Run();
    }
}