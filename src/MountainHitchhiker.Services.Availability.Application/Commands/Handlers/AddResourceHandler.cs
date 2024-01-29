using Convey.CQRS.Commands;
using MountainHitchhiker.Services.Availability.Application.Exceptions;
using MountainHitchhiker.Services.Availability.Application.Services;
using MountainHitchhiker.Services.Availability.Core.Entities;
using MountainHitchhiker.Services.Availability.Core.Repositories;

namespace MountainHitchhiker.Services.Availability.Application.Commands.Handlers;

public class AddResourceHandler : ICommandHandler<AddResource>
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IEventProcessor _eventProcessor;
    
    public AddResourceHandler(
        IResourceRepository resourceRepository,
        IEventProcessor eventProcessor)
    {
        _resourceRepository = resourceRepository;
        _eventProcessor = eventProcessor;
    }
    
    public async Task HandleAsync(
        AddResource command, 
        CancellationToken cancellationToken = new())
    {
        if (await _resourceRepository.ExistsAsync(command.ResourceId))
            throw new ResourceAlreadyExistsException(command.ResourceId);

        var resource = Resource.Create(command.ResourceId, command.Tags);
        await _resourceRepository.AddAsync(resource);
        await _eventProcessor.ProcessAsync(resource.Events);
    }
}