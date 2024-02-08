using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Cache;
using ProductApplication.Products.Errors;
using ProductApplication.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Products.Commands.UpdatePrice;

public class ProductsUpdatePriceCommandHandler : ICommandHandler<ProductsUpdatePriceCommand, Result<ProductResultResponse, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public ProductsUpdatePriceCommandHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsUpdatePriceCommand request, CancellationToken cancellationToken)
    {

        if (request.Price < 0)
        {
            return ProductErrors.PriceCanNotBeLessZero(request.Price);
        }

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(request.Id);
        }

        product.Price = request.Price;
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(ProductKeys.All(), cancellationToken);
        await _cache.RemoveAsync(ProductKeys.Id(request.Id), cancellationToken);

        return _mapper.Map<ProductResultResponse>(product)!;
    }
}

