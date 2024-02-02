namespace Contracts.Products;

public record ProductCreateRequest(string Name, decimal Price, string? Description = null, int? CategoryId = null);

