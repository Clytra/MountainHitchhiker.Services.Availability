using Convey.CQRS.Commands;

namespace MountainHitchhiker.Services.Availability.Application.Commands.Handlers;

public class AddResourceHandler : ICommandHandler<AddResource>
{
    public Task HandleAsync(
        AddResource command, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.CompletedTask;
    }
}