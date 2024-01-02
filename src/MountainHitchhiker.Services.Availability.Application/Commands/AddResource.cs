using Convey.CQRS.Commands;

namespace MountainHitchhiker.Services.Availability.Application.Commands;

public class AddResource : ICommand
{
    public Guid ResourceId { get; }
    public IEnumerable<string> Tags { get; }

    public AddResource(Guid resourceId, IEnumerable<string> tags)
    {
        ResourceId = resourceId;
        Tags = tags;
    }
}