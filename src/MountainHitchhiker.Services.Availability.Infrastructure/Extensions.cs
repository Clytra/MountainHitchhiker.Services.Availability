using Convey;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MountainHitchhiker.Services.Availability.Core.Repositories;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Documents;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Repositories;

namespace MountainHitchhiker.Services.Availability.Infrastructure;

public static class Extensions
{
    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        builder.Services.AddTransient<IResourceRepository, ResourceMongoRepository>();
        
        builder
            .AddMongo()
            .AddMongoRepository<ResourceDocument, Guid>("resources");
        
        return builder;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseConvey();
        
        return app;
    }
}