namespace Contracts.Stocks.Requests;

public record StockExportFromStoreRequest(Guid ProductId, Guid StoreId, int Quantity);
