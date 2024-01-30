using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MountainHitchhiker.Services.Availability.Application;
using MountainHitchhiker.Services.Availability.Application.Events;
using MountainHitchhiker.Services.Availability.Application.Events.External;
using MountainHitchhiker.Services.Availability.Application.Services;
using MountainHitchhiker.Services.Availability.Core.Repositories;
using MountainHitchhiker.Services.Availability.Infrastructure.Decorators;
using MountainHitchhiker.Services.Availability.Infrastructure.Exceptions;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Documents;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Repositories;
using MountainHitchhiker.Services.Availability.Infrastructure.Services;

namespace MountainHitchhiker.Services.Availability.Infrastructure;

public static class Extensions
{
    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        builder.Services.AddTransient<IResourceRepository, ResourcesMongoRepository>();
        builder.Services.AddTransient<IMessageBroker, MessageBroker>();
        builder.Services.AddTransient<IEventProcessor, EventProcessor>();
        builder.Services.AddSingleton<IEventMapper, EventMapper>();
        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
        builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));

        builder.Services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        
        builder
            .AddQueryHandlers()
            .AddInMemoryQueryDispatcher()
            .AddErrorHandler<ExceptionToResponseMapper>()
            .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
            .AddMongo()
            .AddMongoRepository<ResourceDocument, Guid>("resources")
            .AddRabbitMq()
            .AddSwaggerDocs()
            .AddWebApiSwaggerDocs()
            .AddMessageOutbox(o => o.AddMongo());
        
        return builder;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app
            .UseErrorHandler()
            .UseConvey()
            .UsePublicContracts<ContractAttribute>()
            .UseSwaggerDocs()
            .UseRabbitMq()
            .SubscribeEvent<SignedUp>();
        
        return app;
    }
}