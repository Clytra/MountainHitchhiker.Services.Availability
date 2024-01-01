using MountainHitchhiker.Services.Availability.Core.Entities;

namespace MountainHitchhiker.Services.Availability.Core.Events;

public class ResourceDeleted : IDomainEvent
{
    public Resource Resource { get; }

    public ResourceDeleted(Resource resource)
        => Resource = resource;
}