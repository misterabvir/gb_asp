using Contracts.Stores.Requests;
using Contracts.Stores.Responses;
using ExternalLinks;
using ExternalLinks.Base;

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
    => await _clientService.Get<IEnumerable<StoreResponse>>(Linker.Base.Stores.GetAll.Url);

    public async Task<StoreResponse?> GetStore(StoreGetByIdRequest request)
        => await _clientService.Get<StoreResponse>(Linker.Base.Stores.GetById.Url + request.Id);
}
