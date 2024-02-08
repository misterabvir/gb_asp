using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Responses;
using StoreApplication.Stores.Commands.Create;
using StoreDomain;

namespace StoreApplication.Stocks.Commands.MoveBetweenStores;

public sealed record StockMoveBetweenStoresCommand(Guid ProductId, Guid SenderStoreId, Guid TargetStoreId, int Quantity)
        :ICommand<Result<IEnumerable<StockResultResponse>, Error>>;
