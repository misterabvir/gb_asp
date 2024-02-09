using AutoMapper;
using Contracts.Categories.Requests;
using Contracts.Categories.Responses;
using Microsoft.EntityFrameworkCore;
using ProductsApi.BusinessLogicalLayer.Services.Base;
using ProductsApi.DataAccessLayer.Contexts;
using ProductsApi.DataAccessLayer.Models;

namespace ProductsApi.BusinessLogicalLayer.Services;

public class CategoryService : ICategoryService
{
    private const string CATEGORIES = "categories";
    private const string CATEGORY = "category_";
    private readonly ICacheService _cache;
    private readonly ProductsContext _context;
    private readonly IMapper _mapper;

    public CategoryService(ICacheService cache, ProductsContext context, IMapper mapper)
    {
        _cache = cache;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponse>> GetCategories()
    {
        IEnumerable<CategoryResponse>? response = await _cache.Get<IEnumerable<CategoryResponse>>(CATEGORIES);
        if (response is null)
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            response = categories.Select(_mapper.Map<CategoryResponse>);
            await _cache.Set(CATEGORIES, response);
        }
        return response;
    }

    public async Task<CategoryResponse?> GetCategoryById(CategoryGetByIdRequest request)
    {
        CategoryResponse? response = await _cache.Get<CategoryResponse>(CATEGORY + request.Id);
        if (response is not null)
        {
            return response;
        }

        var categories = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
        response = _mapper.Map<CategoryResponse>(categories);

        await _cache.Set(CATEGORY + request.Id, response);

        return response;
    }

    public async Task<IResult> CreateCategory(CategoryCreateRequest request)
    {
        var category = _mapper.Map<Category>(request);
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        await _cache.Remove(CATEGORIES);
        return Results.Ok(category.Id);
    }

    public async Task<IResult> UpdateNameCategory(CategoryUpdateNameRequest request)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (category is null)
        {
            return Results.NotFound($"Not found category with id {request.Id} in database");
        }

        category.Name = request.Name;
        await _context.SaveChangesAsync();
        await _cache.Remove(CATEGORIES);
        await _cache.Remove(CATEGORY + request.Id);
        return Results.Ok();
    }
    public async Task<IResult> DeleteCategory(CategoryDeleteRequest request)
    {
        if (await _context.Products.AnyAsync(p => p.CategoryId == request.Id))
        {
            return Results.BadRequest("Category has products");
        }

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (category is null)
        {
            return Results.NotFound($"Not found category with id {request.Id} in database");
        }
        _context.Remove(category);
        await _context.SaveChangesAsync();

        await _cache.Remove(CATEGORIES);
        await _cache.Remove(CATEGORY + request.Id);
        return Results.Ok();
    }
}
