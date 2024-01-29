using Convey.CQRS.Events;
using MountainHitchhiker.Services.Availability.Core.Events;

namespace MountainHitchhiker.Services.Availability.Application.Services;

public interface IEventMapper
{
    public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    public IEvent Map(IDomainEvent @event);
}