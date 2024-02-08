using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Responses;

namespace StoreApplication.Stores.Commands.UpdateIdentityNumber;

public sealed record StoresUpdateIdentityNumberCommand(Guid Id, string IdentityNumber) : ICommand<Result<StoreResultResponse, Error>>;
