using Convey.CQRS.Commands;
using MountainHitchhiker.Services.Availability.Application.Commands;

namespace MountainHitchhiker.Services.Availability.Api.Endpoints;

public static class ResourceEndpoints
{
    public static void MapResourceEndpoints(this WebApplication app)
    {
        app.MapPost("/resources", async (
            ICommandDispatcher dispatcher, 
            AddResource command) =>
        {
            await dispatcher.SendAsync(command);
            return Results.Accepted();
        });
    }
}