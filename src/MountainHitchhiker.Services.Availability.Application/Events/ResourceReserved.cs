using Convey.CQRS.Events;

namespace MountainHitchhiker.Services.Availability.Application.Events;

[Contract]
public class ResourceReserved : IEvent
{
    public Guid ResourceId { get; }
    public DateTime DateTime { get; }

    public ResourceReserved(Guid resourceId, DateTime dateTime)
    {
        ResourceId = resourceId;
        DateTime = dateTime;
    }
}