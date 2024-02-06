using Application.Abstractions;
using Application.Base;
using Application.Products.Commands.UpdateDescription;
using Application.Products.Responses;

namespace Application.Products.Commands.UpdatePrice;

public record ProductsUpdatePriceCommand(int Id, decimal Price) :ICommand<Result<ProductResultResponse, Error>>;

