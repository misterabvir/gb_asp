using Contracts.Products.Requests;
using Contracts.Products.Responses;

namespace ProductsApi.BusinessLogicalLayer.Services.Base;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetProducts();
    Task<IEnumerable<ProductResponse>> GetProductByCategoryId(ProductGetByCategoryIdRequest request);
    Task<ProductResponse?> GetProductById(ProductGetByIdRequest request);
    Task<Guid> CreateProduct(ProductCreateRequest request);
    Task<bool> UpdateNameProduct(ProductUpdateNameRequest request);
    Task<bool> UpdatePriceProduct(ProductUpdatePriceRequest request);
    Task<bool> UpdateDescriptionProduct(ProductUpdateDescriptionRequest request);
    Task<bool> UpdateCategoryProduct(ProductUpdateCategoryRequest request);
    Task<bool> DeleteProduct(ProductDeleteRequest request);
}

