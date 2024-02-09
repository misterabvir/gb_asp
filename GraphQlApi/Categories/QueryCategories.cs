using Contracts.Categories.Requests;
using Contracts.Categories.Responses;
using GraphQlApi.Services;

namespace GraphQlApi.Categories;

[ExtendObjectType("Query")]
public class QueryCategories
{
    private readonly IHttpClientService _clientService;

    public QueryCategories(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IEnumerable<CategoryResponse>?> GetAllCategories()
    => await _clientService.Get<IEnumerable<CategoryResponse>>("https://localhost:20000/product-api/categories/get_all");

    public async Task<CategoryResponse?> GetCategory(CategoryGetByIdRequest request)
        => await _clientService.Get<CategoryResponse>("https://localhost:20000/product-api/categories/get_by_id?id=" + request.Id);
}
