using Convey;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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
            .AddMongoRepository<ResourceDocument, Guid>("resources");
        
        return builder;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app
            .UseErrorHandler()
            .UseConvey();
        
        return app;
    }
}