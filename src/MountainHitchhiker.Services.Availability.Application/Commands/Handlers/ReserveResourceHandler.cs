using Convey.CQRS.Commands;
using MountainHitchhiker.Services.Availability.Application.Exceptions;
using MountainHitchhiker.Services.Availability.Application.Services;
using MountainHitchhiker.Services.Availability.Core.Repositories;
using MountainHitchhiker.Services.Availability.Core.ValueObjects;

namespace MountainHitchhiker.Services.Availability.Application.Commands.Handlers;

public class ReserveResourceHandler : ICommandHandler<ReserveResource>
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IEventProcessor _eventProcessor;

    public ReserveResourceHandler(
        IResourceRepository resourceRepository,
        IEventProcessor eventProcessor)
    {
        _resourceRepository = resourceRepository;
        _eventProcessor = eventProcessor;
    }
    
    public async Task HandleAsync(
        ReserveResource command, CancellationToken cancellationToken = new())
    {
        var resource = await _resourceRepository.GetAsync(command.ResourceId);
        if (resource is null)
            throw new ResourceNotFoundException(command.ResourceId);

        var reservation = new Reservation(command.DateTime, command.Priority);
        resource.AddReservation(reservation);
        await _resourceRepository.UpdateAsync(resource);
        await _eventProcessor.ProcessAsync(resource.Events);
    }
}