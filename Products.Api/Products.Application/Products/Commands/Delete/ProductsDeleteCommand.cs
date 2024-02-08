using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Commands.Delete;

public record ProductsDeleteCommand(Guid Id) : ICommand<Result<ProductResultResponse, Error>>;
