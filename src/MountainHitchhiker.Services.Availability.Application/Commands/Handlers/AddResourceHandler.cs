using Convey.CQRS.Commands;
using MountainHitchhiker.Services.Availability.Application.Exceptions;
using MountainHitchhiker.Services.Availability.Core.Entities;
using MountainHitchhiker.Services.Availability.Core.Repositories;

namespace MountainHitchhiker.Services.Availability.Application.Commands.Handlers;

public class AddResourceHandler : ICommandHandler<AddResource>
{
    private readonly IResourceRepository _resourceRepository;
    
    public AddResourceHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }
    
    public async Task HandleAsync(
        AddResource command, 
        CancellationToken cancellationToken = new())
    {
        if (await _resourceRepository.ExistsAsync(command.ResourceId))
            throw new ResourceAlreadyExistsException(command.ResourceId);

        var resource = Resource.Create(command.ResourceId, command.Tags);
        await _resourceRepository.AddAsync(resource);
    }
}