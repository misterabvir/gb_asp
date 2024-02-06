using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;
using Domain.Base;

namespace Application.Products.Commands.Create;

public record ProductsCreateCommand(string Name, string? Description, decimal Price, int? CategoryId) : ICommand<Result<ProductResultResponse, Error>>;
