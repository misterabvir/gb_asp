using Contracts.Stocks.Requests;
using Contracts.Stocks.Responses;
using ExternalLinks;
using ExternalLinks.Base;

namespace GraphQlApi.Stocks;

[ExtendObjectType("Query")]
public class QueryStocks
{
    private readonly IHttpClientService _clientService;

    public QueryStocks(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IEnumerable<StockResponse>?> GetStocksByProductId(StockGetByProductIdRequest request)
    => await _clientService.Get<IEnumerable<StockResponse>>(Linker.Base.Stocks.GetByProduct.Url + request.Id);

    public async Task<IEnumerable<StockResponse>?> GetStocksByStoreId(StockGetByStoreIdRequest request)
        => await _clientService.Get<IEnumerable<StockResponse>>(Linker.Base.Stocks.GetByStore.Url + request.Id);
}


