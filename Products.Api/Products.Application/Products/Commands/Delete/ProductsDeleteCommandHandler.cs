using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Cache;
using ProductApplication.Products.Errors;
using ProductApplication.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;
using System.Net.Http.Json;

namespace ProductApplication.Products.Commands.Delete;

public class ProductsDeleteCommandHandler : ICommandHandler<ProductsDeleteCommand, Result<ProductResultResponse, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;
    private readonly HttpClient _client;
    private readonly IExternalApiLinks _links;

    public ProductsDeleteCommandHandler(ProductContext context, IMapper mapper, ICacheService cache, HttpClient client, IExternalApiLinks links)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
        _client = client;
        _links = links;
    }

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsDeleteCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(request.Id);
        }

        var data = await _client.GetAsync(_links.ProductContainsInStock + request.Id, cancellationToken);
        var result = await data.Content.ReadFromJsonAsync<bool>(cancellationToken);
        if (result)
        {
            return ProductErrors.ProductContainsInStocks(request.Id);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(ProductKeys.All(), cancellationToken);
        await _cache.RemoveAsync(ProductKeys.Id(request.Id), cancellationToken);

        return _mapper.Map<ProductResultResponse>(product)!;
    }
}
