using Convey.CQRS.Events;

namespace MountainHitchhiker.Services.Availability.Application.Events.Rejected;

[Contract]
public class ResourceReservedRejected : IRejectedEvent
{
    public Guid ResourceId { get; }
    public string Reason { get; }
    public string Code { get; }

    public ResourceReservedRejected(Guid resourceId, string reason, string code)
    {
        ResourceId = resourceId;
        Reason = reason;
        Code = code;
    }
}