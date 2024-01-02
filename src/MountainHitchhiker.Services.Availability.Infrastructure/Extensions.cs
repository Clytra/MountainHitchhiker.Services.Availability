using Convey;
using Microsoft.AspNetCore.Builder;

namespace MountainHitchhiker.Services.Availability.Infrastructure;

public static class Extensions
{
    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        return builder;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseConvey();
        
        return app;
    }
}