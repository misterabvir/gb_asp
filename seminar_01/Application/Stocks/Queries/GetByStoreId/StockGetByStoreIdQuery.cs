using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;

namespace Application.Stocks.Queries.GetByStoreId;

public sealed record StockGetByStoreIdQuery(int StoreId) : IQuery<Result<IEnumerable<StockResultResponse>, Error>>;

