using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Responses;

namespace StoreApplication.Stocks.Queries.GetByStoreId;

public sealed record StockGetByStoreIdQuery(Guid StoreId) : IQuery<Result<IEnumerable<StockResultResponse>, Error>>;

