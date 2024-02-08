using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Responses;

namespace StoreApplication.Stores.Queries.GetById;

public sealed record StoresGetByIdQuery(Guid Id) : IQuery<Result<StoreResultResponse, Error>>;
