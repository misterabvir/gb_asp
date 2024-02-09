using Contracts.Stocks.Requests;
using ExternalLinks;
using ExternalLinks.Base;

namespace GraphQlApi.Stocks;

[ExtendObjectType("Mutation")]
public class MutationStocks
{
    private readonly IHttpClientService _clientService;

    public MutationStocks(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<string> ImportToStore(StockImportToStoreRequest request)
        => await _clientService.Put(Linker.Base.Stocks.ImportToStore.Url, request);

    public async Task<string> ExportFromStore(StockExportFromStoreRequest request)
        => await _clientService.Put(Linker.Base.Stocks.ExportFromStore.Url, request);

    public async Task<string> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request)
        => await _clientService.Put(Linker.Base.Stocks.ExchangeBetweenStores.Url, request);
}


