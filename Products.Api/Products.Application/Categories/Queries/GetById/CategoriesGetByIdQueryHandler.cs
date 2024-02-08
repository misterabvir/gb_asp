using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Cache;
using ProductApplication.Categories.Errors;
using ProductApplication.Categories.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Categories.Queries.GetById;

public sealed class CategoriesGetByIdQueryHandler : IQueryHandler<CategoriesGetByIdQuery, Result<CategoryResultResponse, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public CategoriesGetByIdQueryHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesGetByIdQuery request, CancellationToken cancellationToken)
    {
        CategoryResultResponse? response = await _cache.GetAsync<CategoryResultResponse>(CategoryKeys.Id(request.Id), cancellationToken);       
        if (response is not  null)
        {
            return response!;
        }

        var category = await _context.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (category is null)
        {
            return CategoryErrors.NotFound(request.Id);
        }

        response = _mapper.Map<CategoryResultResponse>(category)!;
        await _cache.SetAsync(CategoryKeys.Id(response.Id), response, cancellationToken);

        return response;
    }
}
