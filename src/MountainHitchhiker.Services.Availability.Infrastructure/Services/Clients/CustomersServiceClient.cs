using Convey.HTTP;
using MountainHitchhiker.Services.Availability.Application.DTO;
using MountainHitchhiker.Services.Availability.Application.Services;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Services.Clients;

internal class CustomersServiceClient : ICustomersServiceClient
{
    private readonly IHttpClient _httpClient;
    private readonly string _url;
    
    public CustomersServiceClient(
        IHttpClient httpClient,
        HttpClientOptions options)
    {
        _httpClient = httpClient;
        _url = options.Services["customers"];
    }

    public Task<CustomerStateDto> GetStateAsync(Guid customerId)
        => _httpClient.GetAsync<CustomerStateDto>($"{_url}/customers/{customerId}/state");
}