using Application.Abstractions;
using Application.Base;
using Application.Stores.Responses;

namespace Application.Stores.Commands.UpdateIdentityNumber;

public sealed record StoresUpdateIdentityNumberCommand(int Id, string IdentityNumber) : ICommand<Result<StoreResultResponse, Error>>;
