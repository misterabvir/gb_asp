using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Responses;

namespace StoreApplication.Stores.Queries.GetAll;

public sealed record StoresGetAllQuery() : IQuery<Result<IEnumerable<StoreResultResponse>, Error>>;
