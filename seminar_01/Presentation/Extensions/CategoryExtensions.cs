using Contracts.Categories;
using Domain;

namespace Presentation.Extensions;

public static class CategoryExtensions
{
    public static CategoryResponse ToResponse(
        this Category category)    
        => new(category.Id, category.Name);

    public static IEnumerable<CategoryResponse> ToResponse(
        this IEnumerable<Category> categories)
        => categories.Select(ToResponse);

    public static Category ToEntity(
        this CategoryCreateRequest request)
        => new() { Name = request.Name };
}
