using Contracts.Stores.Requests;
using GraphQlApi.Services;
using System.Net;

namespace GraphQlApi.Stores;

[ExtendObjectType("Mutation")]
public class MutationStores
{
    private readonly IHttpClientService _clientService;

    public MutationStores(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<HttpStatusCode> CreateStore(StoreCreateRequest request)
        => await _clientService.Post("https://localhost:20000/stores-api/stores/create", request);

    public async Task<HttpStatusCode> UpdateNameStore(StoreUpdateNameRequest request)
        => await _clientService.Put("https://localhost:20000/stores-api/stores/update_name", request);

    public async Task<HttpStatusCode> DeleteStore(StoreDeleteRequest request)
        => await _clientService.Delete("https://localhost:20000/stores-api/stores/delete", request);
}