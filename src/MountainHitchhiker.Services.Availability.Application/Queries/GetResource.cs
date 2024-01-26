using Convey.CQRS.Queries;
using MountainHitchhiker.Services.Availability.Application.DTO;

namespace MountainHitchhiker.Services.Availability.Application.Queries;

public class GetResource : IQuery<ResourceDto>
{
    public Guid ResourceId { get; set; }
}