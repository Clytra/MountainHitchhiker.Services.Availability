using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MountainHitchhiker.Services.Availability.Core.Entities;
using MountainHitchhiker.Services.Availability.Core.Repositories;
using MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Documents;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Mongo.Repositories;

internal sealed class ResourcesMongoRepository : IResourceRepository
{
    private readonly IMongoRepository<ResourceDocument, Guid> _repository;
    
    public ResourcesMongoRepository(IMongoRepository<ResourceDocument, Guid> repository)
    {
        _repository = repository;
    }
    
    public async Task<Resource> GetAsync(AggregateId id)
    {
        var document = await _repository.GetAsync(r => r.Id == id);

        return document?.AsEntity();
    }

    public Task<bool> ExistsAsync(AggregateId id)
        => _repository.ExistsAsync(r => r.Id == id);

    public Task AddAsync(Resource resource)
        => _repository.AddAsync(resource.AsDocument());

    public Task UpdateAsync(Resource resource)
        => _repository.Collection.ReplaceOneAsync(r => 
            r.Id == resource.Id && r.Version < resource.Version, 
            resource.AsDocument());

    public Task DeleteAsync(AggregateId id)
        => _repository.DeleteAsync(id);
}