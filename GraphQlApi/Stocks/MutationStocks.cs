using Contracts.Stocks.Requests;
using GraphQlApi.Services;
using System.Net;

namespace GraphQlApi.Stocks;

[ExtendObjectType("Mutation")]
public class MutationStocks
{
    private readonly IHttpClientService _clientService;

    public MutationStocks(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<HttpStatusCode> ImportToStore(StockImportToStoreRequest request)
        => await _clientService.Put("https://localhost:20000/stocks-api/stocks/import_to_store", request);

    public async Task<HttpStatusCode> ExportFromStore(StockExportFromStoreRequest request)
        => await _clientService.Put("https://localhost:20000/stocks-api/stocks/export_from_store", request);

    public async Task<HttpStatusCode> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request)
        => await _clientService.Put("https://localhost:20000/stocks-api/stocks/exchange_between_stores", request);
}


