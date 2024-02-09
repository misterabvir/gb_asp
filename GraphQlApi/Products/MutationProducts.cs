using Contracts.Products.Requests;
using ExternalLinks;
using ExternalLinks.Base;
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

    public async Task<string> CreateProduct(ProductCreateRequest request)
        => await _clientService.Post(Linker.Base.Products.Create.Url, request);

    public async Task<string> UpdateNameProduct(ProductUpdateNameRequest request) 
        => await _clientService.Put(Linker.Base.Products.UpdateName.Url, request);

    public async Task<string> UpdateDescriptionProduct(ProductUpdateDescriptionRequest request)
        => await _clientService.Put(Linker.Base.Products.UpdateDescription.Url, request);

    public async Task<string> UpdatePriceProduct(ProductUpdatePriceRequest request)
        => await _clientService.Put(Linker.Base.Products.UpdatePrice.Url, request);

    public async Task<string> UpdateCategoryProduct(ProductUpdateCategoryRequest request)
        => await _clientService.Put(Linker.Base.Products.UpdateCategory.Url, request);

    public async Task<string> DeleteProduct(ProductDeleteRequest request)
        => await _clientService.Delete(Linker.Base.Products.Delete.Url, request);
}