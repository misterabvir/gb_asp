using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Cache;
using ProductApplication.Categories.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Categories.Queries.GetAll;

public class CategoriesGetAllQueryHandler : IQueryHandler<CategoriesGetAllQuery, Result<IEnumerable<CategoryResultResponse>, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public CategoriesGetAllQueryHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<CategoryResultResponse>, Error>> Handle(CategoriesGetAllQuery request, CancellationToken cancellationToken)
    {
        List<CategoryResultResponse>? response = await  _cache.GetAsync<List<CategoryResultResponse>>(CategoryKeys.All(), cancellationToken);

        if (response is not null)
        {
            return response!;
        }

        var categories = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);

        response = categories.Select(c => _mapper.Map<CategoryResultResponse>(c)!).ToList()!;
        await _cache.SetAsync(CategoryKeys.All(), response, cancellationToken);

        return response;
    }
}
