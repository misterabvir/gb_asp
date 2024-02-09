namespace Contracts.Stocks.Requests;

public record StockExchangeBetweenStoresRequest(Guid ProductId, Guid StoreSenderId, Guid StoreTargetId, int Quantity);
