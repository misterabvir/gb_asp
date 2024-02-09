
using Contracts.Stocks.Requests;
using Contracts.Stocks.Responses;
using System.Net;

namespace StocksApi.BusinessLogicalLayer.Services.Base;

public interface IStockService
{
    Task<IEnumerable<StockResponse>> GetStocksByProductId(StockGetByProductIdRequest request);
    Task<bool> IsExistStocksByProductId(StockIsExistByProductIdRequest request);
    Task<IEnumerable<StockResponse>> GetStocksByStoreId(StockGetByStoreIdRequest request);
    Task<bool> IsExistStocksByStoreId(StockIsExistByStoreIdRequest request);
    Task<IResult> ImportToStore(StockImportToStoreRequest request);
    Task<IResult> ExportFromStore(StockExportFromStoreRequest request);
    Task<IResult> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request);
}
