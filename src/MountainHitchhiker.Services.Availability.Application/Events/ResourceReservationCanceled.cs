using Convey.CQRS.Events;

namespace MountainHitchhiker.Services.Availability.Application.Events;

public class ResourceReservationCanceled : IEvent
{
    public Guid ResourceId { get; }
    public DateTime DateTime { get; }

    public ResourceReservationCanceled(Guid resourceId, DateTime dateTime)
    {
        ResourceId = resourceId;
        DateTime = dateTime;
    }
}