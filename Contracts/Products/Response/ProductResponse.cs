namespace Contracts.Products.Responses;

public record ProductResponse(Guid Id, string Name, double Price, string Description, Guid CategoryId);
