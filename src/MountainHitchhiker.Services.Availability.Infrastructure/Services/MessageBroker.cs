using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
using MountainHitchhiker.Services.Availability.Application.Services;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Services;

internal sealed class MessageBroker : IMessageBroker
{
    private readonly IBusPublisher _busPublisher;
    private readonly IMessageOutbox _outbox;
    private readonly OutboxOptions _outboxOptions;
    private readonly IMessagePropertiesAccessor _messagePropertiesAccessor;
    private readonly ICorrelationContextAccessor _correlationContextAccessor;
    
    public MessageBroker(
        IBusPublisher busPublisher, 
        IMessageOutbox outbox, 
        OutboxOptions outboxOptions, 
        IMessagePropertiesAccessor messagePropertiesAccessor,
        ICorrelationContextAccessor correlationContextAccessor)
    {
        _busPublisher = busPublisher;
        _outbox = outbox;
        _outboxOptions = outboxOptions;
        _messagePropertiesAccessor = messagePropertiesAccessor;
        _correlationContextAccessor = correlationContextAccessor;
    }

    public async Task PublishAsync(params IEvent[] events)
        => PublishAsync(events?.AsEnumerable());

    public async Task PublishAsync(IEnumerable<IEvent> events)
    {
        if (events is null) return;

        var messageProperties = _messagePropertiesAccessor.MessageProperties;
        var originatedMessageId = messageProperties?.MessageId;
        var correlationId = messageProperties?.CorrelationId;

        foreach (var @event in events)
        {
            if (@event is null) continue;

            var messageId = Guid.NewGuid().ToString("N");

            if (_outbox.Enabled)
            {
                await _outbox.SendAsync(@event, originatedMessageId, messageId, correlationId);
                continue;
            }
                    
            await _busPublisher.PublishAsync(@event, messageId, correlationId);
        }
    }
}