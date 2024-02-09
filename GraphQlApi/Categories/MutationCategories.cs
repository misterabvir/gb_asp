using Contracts.Categories.Requests;
using ExternalLinks;
using ExternalLinks.Base;

namespace GraphQlApi.Categories;
[ExtendObjectType("Mutation")]
public class MutationCategories
{
    private readonly IHttpClientService _clientService;

    public MutationCategories(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<string> CreateCategory(CategoryCreateRequest request)
        => await _clientService.Post(Linker.Base.Categories.Create.Url, request);

    public async Task<string> UpdateNameCategory(CategoryUpdateNameRequest request)
        => await _clientService.Put(Linker.Base.Categories.UpdateName.Url, request);

    public async Task<string> DeleteCategory(CategoryDeleteRequest request)
        => await _clientService.Delete(Linker.Base.Categories.Delete.Url, request);
}