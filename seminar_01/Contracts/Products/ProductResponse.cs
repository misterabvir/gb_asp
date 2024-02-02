using Contracts.Categories;
using Contracts.Stores;

namespace Contracts.Products;

public record ProductResponse(int Id, string Name, string Description, decimal Price, string CategoryName, List<StoreResponse> Stores);


