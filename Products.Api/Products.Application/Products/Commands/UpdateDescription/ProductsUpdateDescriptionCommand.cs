using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Commands.UpdateDescription;

public record ProductsUpdateDescriptionCommand(Guid Id, string Description) : ICommand<Result<ProductResultResponse, Error>>;
