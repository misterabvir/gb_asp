using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;
using Application.Stores.Commands.Create;
using Domain;

namespace Application.Stocks.Commands.MoveBetweenStores;

public sealed record StockMoveBetweenStoresCommand(int ProductId, int SenderStoreId, int TargetStoreId, int Quantity)
        :ICommand<Result<IEnumerable<StockResultResponse>, Error>>;
