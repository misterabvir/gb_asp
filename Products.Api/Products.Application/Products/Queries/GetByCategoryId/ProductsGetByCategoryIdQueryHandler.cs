using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Errors;
using ProductApplication.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;
using ProductApplication.Products.Cache;

namespace ProductApplication.Products.Queries.GetByCategoryId;

public class ProductsGetByCategoryIdQueryHandler : IQueryHandler<ProductsGetByCategoryIdQuery, Result<IEnumerable<ProductResultResponse>, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public ProductsGetByCategoryIdQueryHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<ProductResultResponse>, Error>> Handle(ProductsGetByCategoryIdQuery query, CancellationToken cancellationToken)
    {
        if (query.CategoryId is not null && !(await _context.Categories.AnyAsync(x => x.Id == query.CategoryId, cancellationToken: cancellationToken)))
        {
            return CategoryErrors.NotFound(query.CategoryId.Value);
        }
        
        var response = (await _cache.GetAsync<List<ProductResultResponse>>(ProductKeys.All(), cancellationToken))?
            .Where(x => x.CategoryId == query.CategoryId).ToList();
        if(response is not null)
        {
            return response;
        } 
        
        var products = await _context.Products
            .Where(x => x.CategoryId == query.CategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
        
        response = products.Select(_mapper.Map<ProductResultResponse>).ToList()!;
        return response;
    }
}
