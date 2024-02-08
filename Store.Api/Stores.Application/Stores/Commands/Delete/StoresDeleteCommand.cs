using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Responses;

namespace StoreApplication.Stores.Commands.Delete;

public sealed record StoresDeleteCommand(Guid Id) : ICommand<Result<StoreResultResponse, Error>>;

