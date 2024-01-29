using Convey.MessageBrokers.RabbitMQ;
using MountainHitchhiker.Services.Availability.Application.Commands;
using MountainHitchhiker.Services.Availability.Application.Events.Rejected;
using MountainHitchhiker.Services.Availability.Application.Exceptions;
using MountainHitchhiker.Services.Availability.Core.Exceptions;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Exceptions;

internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
{
    public object Map(Exception exception, object message)
        => exception switch
        {
            MissingResourceTagsException ex => 
                new AddResourceRejected(Guid.Empty, ex.Message, ex.Code),
            InvalidResourceTagsException ex => 
                new AddResourceRejected(Guid.Empty, ex.Message, ex.Code),
            CannotExpropriateReservationException ex => 
                new ResourceReservedRejected(ex.ResourceId, ex.Message, ex.Code),
            ResourceAlreadyExistsException ex =>
                new AddResourceRejected(ex.ResourceId, ex.Message, ex.Code),
            ResourceNotFoundException ex => message switch
            {
                ReserveResource cmd => 
                    new ResourceReservedRejected(ex.ResourceId, ex.Message, ex.Code),
                _ => null
            },
            _ => null
        };
}