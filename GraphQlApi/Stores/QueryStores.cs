using Contracts.Stores.Requests;
using Contracts.Stores.Responses;
using GraphQlApi.Services;

namespace GraphQlApi.Stores;

[ExtendObjectType("Query")]
public class QueryStores
{
    private readonly IHttpClientService _clientService;

    public QueryStores(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IEnumerable<StoreResponse>?> GetAllStores()
    => await _clientService.Get<IEnumerable<StoreResponse>>("https://localhost:20000/stores-api/stores/get_all");

    public async Task<StoreResponse?> GetStore(StoreGetByIdRequest request)
        => await _clientService.Get<StoreResponse>("https://localhost:20000/stores-api/stores/get_by_id?id=" + request.Id);
}
