using Contracts.Categories.Requests;
using Contracts.Categories.Responses;
using ExternalLinks;
using ExternalLinks.Base;

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
    => await _clientService.Get<IEnumerable<CategoryResponse>>(Linker.Base.Categories.GetAll.Url);

    public async Task<CategoryResponse?> GetCategory(CategoryGetByIdRequest request)
        => await _clientService.Get<CategoryResponse>(Linker.Base.Categories.GetById.Url + request.Id);
}
