using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;

namespace Application.Stocks.Queries.GetByProductId;

public sealed record StockGetByProductIdQuery(int ProductId) : IQuery<Result<IEnumerable<StockResultResponse>, Error>>;
