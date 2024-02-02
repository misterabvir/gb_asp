namespace Contracts.Products;

public record ProductUpdateCategoryRequest(int Id, int? CategoryId = null);

