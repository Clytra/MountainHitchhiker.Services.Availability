using Convey.CQRS.Commands;

namespace MountainHitchhiker.Services.Availability.Application.Commands;

public class ReserveResource : ICommand
{
    public Guid ResourceId { get; }
    public DateTime DateTime { get; }
    public int Priority { get; }
    public Guid CustomerId { get; }

    public ReserveResource(
        Guid resourceId,
        DateTime dateTime,
        int priority)
    {
        ResourceId = resourceId;
        DateTime = dateTime;
        Priority = priority;
    }
}