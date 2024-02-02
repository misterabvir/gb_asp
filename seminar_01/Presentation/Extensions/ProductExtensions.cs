using Contracts.Products;
using Domain;

namespace Presentation.Extensions;

public static class ProductExtensions
{
    public static ProductResponse ToResponse(this Product product)
        => new(
            product.Id,
            product.Name,
            product.Description ?? string.Empty,
            product.Price,
            product.Category?.ToResponse().Name ?? "none",
            product.Stocks?.Select(s => s.Store!).Select(s=>s.ToResponse()).ToList() ?? []);

    public static IEnumerable<ProductResponse> ToResponse(this IEnumerable<Product> products)
    => products.Select(ToResponse);

    public static Product ToEntity(this ProductCreateRequest request)
    => new() { 
         Name = request.Name,
         Description = request.Description,
         Price = request.Price,
         CategoryId = request.CategoryId
    };
}