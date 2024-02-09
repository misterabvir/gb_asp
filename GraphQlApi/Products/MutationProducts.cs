using Contracts.Products.Requests;
using GraphQlApi.Services;
using System.Net;

namespace GraphQlApi.GraphQl;

[ExtendObjectType("Mutation")]
public class MutationProducts
{
    private readonly IHttpClientService _clientService;

    public MutationProducts(IHttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<HttpStatusCode> CreateProduct(ProductCreateRequest request)
        => await _clientService.Post("https://localhost:20000/product-api/products/create", request);

    public async Task<HttpStatusCode> UpdateNameProduct(ProductUpdateNameRequest request) 
        => await _clientService.Put("https://localhost:20000/product-api/products/update_name", request);

    public async Task<HttpStatusCode> UpdateDescriptionProduct(ProductUpdateDescriptionRequest request)
        => await _clientService.Put("https://localhost:20000/product-api/products/update_description", request);

    public async Task<HttpStatusCode> UpdatePriceProduct(ProductUpdatePriceRequest request)
        => await _clientService.Put("https://localhost:20000/product-api/products/update_price", request);

    public async Task<HttpStatusCode> UpdateCategoryProduct(ProductUpdateCategoryRequest request)
        => await _clientService.Put("https://localhost:20000/product-api/products/update_category", request);

    public async Task<HttpStatusCode> DeleteProduct(ProductDeleteRequest request)
        => await _clientService.Delete("https://localhost:20000/product-api/products/delete", request);
}