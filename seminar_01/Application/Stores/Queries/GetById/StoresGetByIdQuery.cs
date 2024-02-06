using Application.Abstractions;
using Application.Base;
using Application.Stores.Responses;

namespace Application.Stores.Queries.GetById;

public sealed record StoresGetByIdQuery(int Id) : IQuery<Result<StoreResultResponse, Error>>;
