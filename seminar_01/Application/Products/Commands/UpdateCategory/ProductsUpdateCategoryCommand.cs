using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;
namespace Application.Products.Commands.UpdateCategory;

public record ProductsUpdateCategoryCommand(int Id, int? CategoryId) : ICommand<Result<ProductResultResponse, Error>>;
