namespace Contracts.Stocks.Requests;

public record StockImportToStoreRequest(Guid ProductId, Guid StoreId, int Quantity);
