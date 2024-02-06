using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;

namespace Application.Stocks.Commands.ImportToStore;

public sealed record StockImportToStoreCommand(int ProductId, int StoreId, int Quantity) : ICommand<Result<StockResultResponse, Error>>;
