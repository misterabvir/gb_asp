using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;

namespace Application.Stocks.Commands.ExportFromStore;

public sealed record StockExportFromStoreCommand(int ProductId, int StoreId, int Quantity) : ICommand<Result<StockResultResponse, Error>>;
