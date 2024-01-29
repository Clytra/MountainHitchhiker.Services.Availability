using MountainHitchhiker.Services.Availability.Core.Events;

namespace MountainHitchhiker.Services.Availability.Application.Events;

public interface IDomainEventHandler<in T> where T : class, IDomainEvent
{
    Task HandleAsync(T @event);
}