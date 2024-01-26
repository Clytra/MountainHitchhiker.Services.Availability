using Convey.CQRS.Queries;
using MongoDB.Driver;
using MountainHitchhiker.Services.Availability.Application.DTO;
using MountainHitchhiker.Services.Availability.Application.Queries;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Documents;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Queries.Handlers;

internal sealed class GetResourceHandler : IQueryHandler<GetResource, ResourceDto>
{
    private readonly IMongoDatabase _database;
    
    public GetResourceHandler(IMongoDatabase database)
    {
        _database = database;
    }
    
    public async Task<ResourceDto> HandleAsync(
        GetResource query, CancellationToken cancellationToken = new())
    {
        var document = await _database.GetCollection<ResourceDocument>("resources")
            .Find(r => r.Id == query.ResourceId)
            .SingleOrDefaultAsync();

        return document?.AsDto();
    }
}