using Application.Abstractions;
using Application.Base;
using Application.Products.Commands.UpdateDescription;
using Application.Products.Responses;

namespace Application.Products.Commands.Delete;

public record ProductsDeleteCommand(int Id) : ICommand<Result<ProductResultResponse, Error>>;
