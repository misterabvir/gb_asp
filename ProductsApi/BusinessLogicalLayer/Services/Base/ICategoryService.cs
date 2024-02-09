using Contracts.Categories.Requests;
using Contracts.Categories.Responses;

namespace ProductsApi.BusinessLogicalLayer.Services.Base;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetCategories();
    Task<CategoryResponse?> GetCategoryById(CategoryGetByIdRequest request);
    Task<IResult> CreateCategory(CategoryCreateRequest request);
    Task<IResult> UpdateNameCategory(CategoryUpdateNameRequest request);
    Task<IResult> DeleteCategory(CategoryDeleteRequest request);
}
