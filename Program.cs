using EventSourcing.Endpoints;
using EventSourcing.Persistence;
using Microsoft.EntityFrameworkCore;

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

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
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