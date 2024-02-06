namespace Contracts.Stocks;

public record StockMoveRequest(int ProductId, int FromStoreId, int ToStoreId, int Quantity);

