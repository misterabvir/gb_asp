using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Commands.Create;

public record ProductsCreateCommand(string Name, string? Description, decimal Price, Guid? CategoryId) : ICommand<Result<ProductResultResponse, Error>>;
