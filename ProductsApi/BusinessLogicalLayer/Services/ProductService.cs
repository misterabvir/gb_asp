using AutoMapper;
using Contracts.Products.Requests;
using Contracts.Products.Responses;
using Microsoft.EntityFrameworkCore;
using ProductsApi.BusinessLogicalLayer.Services.Base;
using ProductsApi.DataAccessLayer.Contexts;
using ProductsApi.DataAccessLayer.Models;

namespace ProductsApi.BusinessLogicalLayer.Services;

public class ProductService : IProductService
{
    private const string PRODUCT = "product_";
    private const string PRODUCTS = "products";
    private readonly ICacheService _cache;
    private readonly ProductsContext _context;
    private readonly IExternalQueryService _queryService;
    private readonly IMapper _mapper;

    public ProductService(ICacheService cache, ProductsContext context, IMapper mapper, IExternalQueryService queryService)
    {
        _cache = cache;
        _context = context;
        _mapper = mapper;
        _queryService = queryService;
    }

    public async Task<IEnumerable<ProductResponse>> GetProducts()
    {
        var response = await _cache.Get<IEnumerable<ProductResponse>>(PRODUCTS);
        if (response is null)
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            response = products.Select(_mapper.Map<ProductResponse>);
            await _cache.Set("product", response);
        }
        return response;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductByCategoryId(ProductGetByCategoryIdRequest request)
    {
        var products = await _context.Products.Where(x => x.CategoryId == request.CategoryId).AsNoTracking().ToListAsync();
        var response = products.Select(_mapper.Map<ProductResponse>);
        return response;
    }

    public async Task<ProductResponse?> GetProductById(ProductGetByIdRequest request)
    {
        ProductResponse? response = await _cache.Get<ProductResponse>(PRODUCT + request.Id);
        if (response is null)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id);
            if (product is null)
            {
                return null;
            }
            response = _mapper.Map<ProductResponse>(product);
            await _cache.Set(PRODUCT + request.Id, response);
        }
        return response;
    }

    public async Task<Guid> CreateProduct(ProductCreateRequest request)
    {
        var product = _mapper.Map<Product>(request)!;
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        return product.Id;
    }

    public async Task<bool> UpdateCategoryProduct(ProductUpdateCategoryRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return false;
        }
        product.CategoryId = request.CategoryId;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return true;
    }


    public async Task<bool> UpdateDescriptionProduct(ProductUpdateDescriptionRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return false;
        }
        product.Description = request.Description;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return true;
    }

    public async Task<bool> UpdateNameProduct(ProductUpdateNameRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return false;
        }
        product.Name = request.Name;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return true;
    }

    public async Task<bool> UpdatePriceProduct(ProductUpdatePriceRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return false;
        }
        product.Price = request.Price;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return true;
    }
    public async Task<bool> DeleteProduct(ProductDeleteRequest request)
    {
        if (await _queryService.IsStockExist(request.Id) == true)
        {
            return false;
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return false;
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return true;
    }
}

