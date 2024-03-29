using Convey.CQRS.Events;

namespace MountainHitchhiker.Services.Availability.Application.Events;

[Contract]
public class ResourceAdded : IEvent
{
    public Guid ResourceId { get; }

    public ResourceAdded(Guid resourceId)
    {
        ResourceId = resourceId;
    }
}