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
    private readonly ICustomersServiceClient _customersServiceClient;

    public ReserveResourceHandler(
        IResourceRepository resourceRepository,
        IEventProcessor eventProcessor,
        ICustomersServiceClient customersServiceClient)
    {
        _resourceRepository = resourceRepository;
        _eventProcessor = eventProcessor;
        _customersServiceClient = customersServiceClient;
    }
    
    public async Task HandleAsync(
        ReserveResource command, CancellationToken cancellationToken = new())
    {
        var resource = await _resourceRepository.GetAsync(command.ResourceId);
        if (resource is null)
            throw new ResourceNotFoundException(command.ResourceId);

        var customerState = await _customersServiceClient.GetStateAsync(command.CustomerId);
        if (customerState is null)
            throw new CustomerNotFoundException(command.CustomerId);
        if (customerState.IsValid)
            throw new InvalidCustomerStateException(command.CustomerId, customerState.State);

        var reservation = new Reservation(command.DateTime, command.Priority);
        resource.AddReservation(reservation);
        await _resourceRepository.UpdateAsync(resource);
        await _eventProcessor.ProcessAsync(resource.Events);
    }
}