using MountainHitchhiker.Services.Availability.Core.Entities;

namespace MountainHitchhiker.Services.Availability.Core.Events;

public class ResourceCreated : IDomainEvent
{
    public Resource Resource { get; }

    public ResourceCreated(Resource resource)
        => Resource = resource;
}