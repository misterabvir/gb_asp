using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Commands.UpdateDescription;

public record ProductsUpdateDescriptionCommand(int Id, string Description) : ICommand<Result<ProductResultResponse, Error>>;
