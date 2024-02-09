using Contracts.Products.Requests;
using Contracts.Products.Responses;

namespace ProductsApi.BusinessLogicalLayer.Services.Base;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetProducts();
    Task<IEnumerable<ProductResponse>> GetProductByCategoryId(ProductGetByCategoryIdRequest request);
    Task<ProductResponse?> GetProductById(ProductGetByIdRequest request);
    Task<bool> IsProductExist(ProductIsExistByIdRequest request);
    
    Task<IResult> CreateProduct(ProductCreateRequest request);
    Task<IResult> UpdateNameProduct(ProductUpdateNameRequest request);
    Task<IResult> UpdatePriceProduct(ProductUpdatePriceRequest request);
    Task<IResult> UpdateDescriptionProduct(ProductUpdateDescriptionRequest request);
    Task<IResult> UpdateCategoryProduct(ProductUpdateCategoryRequest request);
    Task<IResult> DeleteProduct(ProductDeleteRequest request);
}

