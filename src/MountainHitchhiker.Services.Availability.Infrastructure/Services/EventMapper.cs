using Convey.CQRS.Events;
using MountainHitchhiker.Services.Availability.Application.Events;
using MountainHitchhiker.Services.Availability.Application.Services;
using MountainHitchhiker.Services.Availability.Core.Events;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Services;

public class EventMapper : IEventMapper
{
    public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);

    public IEvent Map(IDomainEvent @event)
        => @event switch
        {
            ResourceCreated e => new ResourceAdded(e.Resource.Id),
            ReservationCanceled e => new ResourceReservationCanceled(e.Resource.Id, e.Reservation.DateTime),
            ReservationAdded e => new ResourceReserved(e.Resource.Id, e.Reservation.DateTime),
            _ => null
        };
}