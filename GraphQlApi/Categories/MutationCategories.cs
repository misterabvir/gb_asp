using Contracts.Categories.Requests;
using GraphQlApi.Services;
using System.Net;

namespace GraphQlApi.Categories;
[ExtendObjectType("Mutation")]
public class MutationCategories
{
    private readonly IHttpClientService _clientService;

    public MutationCategories(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<HttpStatusCode> CreateCategory(CategoryCreateRequest request)
        => await _clientService.Post("https://localhost:20000/product-api/categories/create", request);

    public async Task<HttpStatusCode> UpdateNameCategory(CategoryUpdateNameRequest request)
        => await _clientService.Put("https://localhost:20000/product-api/categories/update_name", request);

    public async Task<HttpStatusCode> DeleteCategory(CategoryDeleteRequest request)
        => await _clientService.Delete("https://localhost:20000/product-api/categories/delete", request);
}