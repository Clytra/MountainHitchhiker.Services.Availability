using MountainHitchhiker.Services.Availability.Core.Events;

namespace MountainHitchhiker.Services.Availability.Application.Services;

public interface IEventProcessor
{
    Task ProcessAsync(IEnumerable<IDomainEvent> events);
}