using Convey.CQRS.Events;

namespace MountainHitchhiker.Services.Availability.Application.Services;

public interface IMessageBroker
{
    Task PublishAsync(params IEvent[] events);
    Task PublishAsync(IEnumerable<IEvent> events);
}