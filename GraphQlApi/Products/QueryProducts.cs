using Contracts.Products.Requests;
using Contracts.Products.Responses;
using GraphQlApi.Services;

namespace GraphQlApi.GraphQl;

[ExtendObjectType("Query")]
public class QueryProducts
{
    private readonly IHttpClientService _clientService;

    public QueryProducts(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IEnumerable<ProductResponse>?> GetAllProducts()
        => await _clientService.Get<IEnumerable<ProductResponse>>("https://localhost:20000/product-api/products/get_all");

    public async Task<ProductResponse?> GetProduct(ProductGetByIdRequest request)
        => await _clientService.Get<ProductResponse>("https://localhost:20000/product-api/products/get_by_id?id=" + request.Id);

    public async Task<IEnumerable<ProductResponse>?> GetProductsByCategory(ProductGetByCategoryIdRequest request)
       => await _clientService.Get<IEnumerable<ProductResponse>>("https://localhost:20000/product-api/products/get_by_category_id?CategoryId=" + request.CategoryId);
}
