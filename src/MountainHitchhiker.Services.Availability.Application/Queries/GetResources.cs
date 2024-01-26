using Convey.CQRS.Queries;
using MountainHitchhiker.Services.Availability.Application.DTO;

namespace MountainHitchhiker.Services.Availability.Application.Queries;

public class GetResources : IQuery<IEnumerable<ResourceDto>>
{
    public IEnumerable<string> Tags { get; set; }
    public bool MatchAllTags { get; set; }
}