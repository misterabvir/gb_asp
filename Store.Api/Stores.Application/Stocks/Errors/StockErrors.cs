using StoreApplication.Base;
using StoreApplication.Stocks.Responses;

namespace StoreApplication.Stocks.Errors;

public static class StockErrors
{
    internal static NotFound NotFound(Guid productId, Guid storeId) 
        => new("Stock", "Stock for product with id " + productId + " and store id " + storeId + " not found");
    internal static NotFound ProductNotExist(Guid productId)
        => new("Stock", "Product with id " + productId + " not exist");
    internal static Conflict NotEnoughQuantity(Guid productId, Guid storeId, int quantity)
        => new("Stock", "Not enough quantity for product with id " + productId + " and store id " + storeId + " (" + quantity + ")");

    internal static Conflict QuantityMustBePositive(int quantity)
        => new("Stock", "Quantity must be positive (" + quantity + ")");

}
