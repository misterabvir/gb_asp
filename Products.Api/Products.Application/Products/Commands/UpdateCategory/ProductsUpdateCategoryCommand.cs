using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;
namespace ProductApplication.Products.Commands.UpdateCategory;

public record ProductsUpdateCategoryCommand(Guid Id, Guid? CategoryId) : ICommand<Result<ProductResultResponse, Error>>;
