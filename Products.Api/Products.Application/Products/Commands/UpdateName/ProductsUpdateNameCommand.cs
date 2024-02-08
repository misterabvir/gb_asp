using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Commands.UpdateName;

public record ProductsUpdateNameCommand(Guid Id, string Name) : ICommand<Result<ProductResultResponse, Error>>;
