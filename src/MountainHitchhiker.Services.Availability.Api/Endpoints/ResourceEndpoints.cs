using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using MountainHitchhiker.Services.Availability.Application.Commands;
using MountainHitchhiker.Services.Availability.Application.Queries;

namespace MountainHitchhiker.Services.Availability.Api.Endpoints;

public static class ResourceEndpoints
{
    public static void UseResourceEndpoints(this WebApplication app)
    {
        app.MapGet("/resources/{id}", async (
            IQueryDispatcher dispatcher, 
            GetResource query) =>
        {
            var resource = await dispatcher.QueryAsync(query);
            return resource is not null ? Results.Ok(resource) : Results.NotFound();
        });
        
        app.MapGet("/resources", async (
            IQueryDispatcher dispatcher, 
            GetResources query) =>
        {
            var resource = await dispatcher.QueryAsync(query);
            return resource is not null ? Results.Ok(resource) : Results.NotFound();
        });
        
        app.MapPost("/resources", async (
            ICommandDispatcher dispatcher, 
            AddResource command) =>
        {
            await dispatcher.SendAsync(command);

            var resourceLocation = $"/resources/{command.ResourceId}";
            return Results.Created(resourceLocation, new { command.ResourceId });
        });
    }
}