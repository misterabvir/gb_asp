using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Queries.GetAll;
using StoreApplication.Stores.Responses;

namespace StoreApplication.Stores.Commands.Create;

public sealed record StoresCreateCommand(string IdentityNumber) : ICommand<Result<StoreResultResponse, Error>>;


