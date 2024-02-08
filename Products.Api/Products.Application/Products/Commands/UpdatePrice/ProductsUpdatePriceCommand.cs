using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Commands.UpdatePrice;

public record ProductsUpdatePriceCommand(Guid Id, decimal Price) :ICommand<Result<ProductResultResponse, Error>>;

