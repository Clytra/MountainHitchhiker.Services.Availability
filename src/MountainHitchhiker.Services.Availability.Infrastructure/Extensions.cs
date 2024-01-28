using Convey;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MountainHitchhiker.Services.Availability.Application.Events.External;
using MountainHitchhiker.Services.Availability.Core.Repositories;
using MountainHitchhiker.Services.Availability.Infrastructure.Exceptions;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Documents;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Repositories;

namespace MountainHitchhiker.Services.Availability.Infrastructure;

public static class Extensions
{
    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        builder.Services.AddTransient<IResourceRepository, ResourcesMongoRepository>();
        
        builder
            .AddQueryHandlers()
            .AddInMemoryQueryDispatcher()
            .AddErrorHandler<ExceptionToResponseMapper>()
            .AddMongo()
            .AddMongoRepository<ResourceDocument, Guid>("resources")
            .AddRabbitMq();
        
        return builder;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app
            .UseErrorHandler()
            .UseConvey()
            .UseRabbitMq()
            .SubscribeEvent<SignedUp>();
        
        return app;
    }
}