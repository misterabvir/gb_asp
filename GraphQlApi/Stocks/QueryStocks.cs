using Contracts.Stocks.Requests;
using Contracts.Stocks.Responses;
using GraphQlApi.Services;

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
    => await _clientService.Get<IEnumerable<StockResponse>>("https://localhost:20000/stocks-api/stocks/get_by_product_id?ProductId=" + request.ProductId);

    public async Task<IEnumerable<StockResponse>?> GetStocksByStoreId(StockGetByStoreIdRequest request)
        => await _clientService.Get<IEnumerable<StockResponse>>("https://localhost:20000/stocks-api/stocks/get_by_store_id?StoreId=" + request.StoreId);
}


