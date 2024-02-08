using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Responses;

namespace StoreApplication.Stocks.Commands.ImportToStore;

public sealed record StockImportToStoreCommand(Guid ProductId, Guid StoreId, int Quantity) : ICommand<Result<StockResultResponse, Error>>;
