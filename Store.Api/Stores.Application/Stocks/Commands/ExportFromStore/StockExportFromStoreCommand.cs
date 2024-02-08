using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Responses;

namespace StoreApplication.Stocks.Commands.ExportFromStore;

public sealed record StockExportFromStoreCommand(Guid ProductId, Guid StoreId, int Quantity) : ICommand<Result<StockResultResponse, Error>>;
