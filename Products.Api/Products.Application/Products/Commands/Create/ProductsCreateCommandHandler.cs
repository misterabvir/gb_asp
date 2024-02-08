using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Errors;
using ProductApplication.Products.Cache;
using ProductApplication.Products.Errors;
using ProductApplication.Products.Responses;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Products.Commands.Create;

public class ProductsCreateCommandHandler : ICommandHandler<ProductsCreateCommand, Result<ProductResultResponse, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public ProductsCreateCommandHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsCreateCommand request, CancellationToken cancellationToken)
    {

        if (request.CategoryId is not null)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken: cancellationToken);
            if (category is null)
            {
                return CategoryErrors.NotFound(request.CategoryId.Value);
            }
        }
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
        if (product is not null)
        {
            return ProductErrors.AlreadyExist(request.Name);
        }

        product = _mapper.Map<Product>(request)!;
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(ProductKeys.All(), cancellationToken);

        return _mapper.Map<ProductResultResponse>(product)!;

    }
}
