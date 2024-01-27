using Convey.CQRS.Commands;
using MountainHitchhiker.Services.Availability.Application.Exceptions;
using MountainHitchhiker.Services.Availability.Core.Repositories;
using MountainHitchhiker.Services.Availability.Core.ValueObjects;

namespace MountainHitchhiker.Services.Availability.Application.Commands.Handlers;

public class ReserveResourceHandler : ICommandHandler<ReserveResource>
{
    private readonly IResourceRepository _resourceRepository;

    public ReserveResourceHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
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
    }
}