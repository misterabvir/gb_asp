namespace Contracts.Products.Requests;

public record ProductCreateRequest(string Name, double Price, string Description, Guid CategoryId);

