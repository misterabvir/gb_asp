using Contracts.Products.Requests;
using Contracts.Products.Responses;
using ExternalLinks;
using ExternalLinks.Base;

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
        => await _clientService.Get<IEnumerable<ProductResponse>>(Linker.Base.Products.GetAll.Url);

    public async Task<ProductResponse?> GetProduct(ProductGetByIdRequest request)
        => await _clientService.Get<ProductResponse>(Linker.Base.Products.GetById.Url + request.Id);

    public async Task<IEnumerable<ProductResponse>?> GetProductsByCategory(ProductGetByCategoryIdRequest request)
        => await _clientService.Get<IEnumerable<ProductResponse>>(Linker.Base.Products.GetByCategory.Url + request.CategoryId);
}
