using MountainHitchhiker.Services.Availability.Application.DTO;

namespace MountainHitchhiker.Services.Availability.Application.Services;

public interface ICustomersServiceClient
{
    Task<CustomerStateDto> GetStateAsync(Guid customerId);
}