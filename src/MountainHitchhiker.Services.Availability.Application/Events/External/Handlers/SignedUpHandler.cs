using Convey.CQRS.Events;

namespace MountainHitchhiker.Services.Availability.Application.Events.External.Handlers;

public class SignedUpHandler : IEventHandler<SignedUp>
{
    public Task HandleAsync(
        SignedUp @event, CancellationToken cancellationToken = new())
    {
        return Task.CompletedTask;
    }
}