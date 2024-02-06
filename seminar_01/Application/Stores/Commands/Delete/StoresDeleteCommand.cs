using Application.Abstractions;
using Application.Base;
using Application.Stores.Responses;

namespace Application.Stores.Commands.Delete;

public sealed record StoresDeleteCommand(int Id) : ICommand<Result<StoreResultResponse, Error>>;

