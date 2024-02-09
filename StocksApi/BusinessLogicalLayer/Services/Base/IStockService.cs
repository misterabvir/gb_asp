
using Contracts.Stocks.Requests;
using Contracts.Stocks.Responses;

namespace StocksApi.BusinessLogicalLayer.Services.Base;

public interface IStockService
{
    Task<IEnumerable<StockResponse>> GetStocksByProductId(StockGetByProductIdRequest request);
    Task<IEnumerable<StockResponse>> GetStocksByStoreId(StockGetByStoreIdRequest request);
    Task<bool> ImportToStore(StockImportToStoreRequest request);
    Task<bool> ExportFromStore(StockExportFromStoreRequest request);
    Task<bool> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request);
}
