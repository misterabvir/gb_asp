namespace Contracts.Stocks.Responses;

public record StockResponse(Guid ProductId, Guid StoreId, int Quantity);
