using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Cache;
using ProductApplication.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Products.Queries.GetAll;

public class ProductsGetAllQueryHandler : IQueryHandler<ProductsGetAllQuery, Result<IEnumerable<ProductResultResponse>, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public ProductsGetAllQueryHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<ProductResultResponse>, Error>> Handle(ProductsGetAllQuery request, CancellationToken cancellationToken)
    {
        List<ProductResultResponse>? response = await _cache.GetAsync<List<ProductResultResponse>>(ProductKeys.All(), cancellationToken);
        
        if (response is not null)
        {
            return response;
        }

        var products = await _context.Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

        response = products.Select(_mapper.Map<ProductResultResponse>).ToList()!;
        await _cache.SetAsync(ProductKeys.All(), response, cancellationToken);

        return response;
    }
}
