using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Responses;

namespace StoreApplication.Stocks.Queries.GetByProductId;

public sealed record StockGetByProductIdQuery(Guid ProductId) : IQuery<Result<IEnumerable<StockResultResponse>, Error>>;
