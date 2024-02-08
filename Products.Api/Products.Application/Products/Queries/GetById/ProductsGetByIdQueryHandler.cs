using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Cache;
using ProductApplication.Products.Errors;
using ProductApplication.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductPersistence.Contexts;

namespace ProductApplication.Products.Queries.GetById;

public class ProductsGetByIdQueryHandler : IQueryHandler<ProductsGetByIdQuery, Result<ProductResultResponse, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsGetByIdQueryHandler(ProductContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public IMemoryCache Cache => _cache;

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsGetByIdQuery query, CancellationToken cancellationToken)
    {
        if(_cache.TryGetValue(ProductKeys.Id(query.Id), out ProductResultResponse? response))
        {
            return response!;
        }

        var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => 
                x.Id == query.Id, 
                cancellationToken: cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(query.Id);
        }

        response = _mapper.Map<ProductResultResponse>(product)!;
        _cache.Set(ProductKeys.Id(query.Id), response);

        return response;

    }
}
