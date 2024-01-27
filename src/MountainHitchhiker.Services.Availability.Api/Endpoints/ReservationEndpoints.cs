using Convey.CQRS.Commands;
using MountainHitchhiker.Services.Availability.Application.Commands;

namespace MountainHitchhiker.Services.Availability.Api.Endpoints;

public static class ReservationEndpoints
{
    public static void UseReservationEndpoints(this WebApplication app)
    {
        app.MapPost("/resources/{resourceId}/reservations/{dateTime}", async (
            ICommandDispatcher dispatcher, 
            ReserveResource command) =>
        {
            await dispatcher.SendAsync(command);
        });
    }
}