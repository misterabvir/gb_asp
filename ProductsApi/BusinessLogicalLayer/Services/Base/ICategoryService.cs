using Contracts.Categories.Requests;
using Contracts.Categories.Responses;

namespace ProductsApi.BusinessLogicalLayer.Services.Base;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetCategories();
    Task<CategoryResponse?> GetCategoryById(CategoryGetByIdRequest request);
    Task<Guid> CreateCategory(CategoryCreateRequest request);
    Task<bool> UpdateNameCategory(CategoryUpdateNameRequest request);
    Task<bool> DeleteCategory(CategoryDeleteRequest request);
}
