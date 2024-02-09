using AutoMapper;
using Contracts.Products.Requests;
using Contracts.Products.Responses;
using ExternalLinks;
using ExternalLinks.Base;
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
    private readonly IHttpClientService _query;
    private readonly IMapper _mapper;

    public ProductService(ICacheService cache, ProductsContext context, IMapper mapper, IHttpClientService query)
    {
        _cache = cache;
        _context = context;
        _mapper = mapper;
        _query = query;
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

    public async Task<bool> IsProductExist(ProductIsExistByIdRequest request)
    {
        ProductResponse? response = await _cache.Get<ProductResponse>(PRODUCT + request.Id);
        if (response is not null)
        {
            return true;
        }
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id);
        return product is not null;
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

    public async Task<IResult> CreateProduct(ProductCreateRequest request)
    {
        var product = _mapper.Map<Product>(request)!;
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        return Results.Created(string.Empty, product.Id);
    }

    public async Task<IResult> UpdateCategoryProduct(ProductUpdateCategoryRequest request)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId);
        if (category is null)
        {
            return Results.NotFound($"Not found category with id {request.CategoryId}");
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return Results.NotFound($"Not found product with id {request.Id}");
        }
        product.CategoryId = request.CategoryId;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return Results.Ok();
    }


    public async Task<IResult> UpdateDescriptionProduct(ProductUpdateDescriptionRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return Results.NotFound($"Not found product with id {request.Id}");
        }
        product.Description = request.Description;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return Results.Ok();
    }

    public async Task<IResult> UpdateNameProduct(ProductUpdateNameRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return Results.NotFound($"Not found product with id {request.Id}");
        }
        product.Name = request.Name;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return Results.Ok();
    }

    public async Task<IResult> UpdatePriceProduct(ProductUpdatePriceRequest request)
    {
        if(request.Price < 0)
        {
            return Results.BadRequest("Price must be greater than 0");
        }
        
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return Results.NotFound($"Not found product with id {request.Id}");
        }
        product.Price = request.Price;
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return Results.Ok();
    }

    public async Task<IResult> DeleteProduct(ProductDeleteRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
        {
            return Results.NotFound($"Not found product with id {request.Id}");
        }

        if (await _query.Get<bool>(Linker.Base.Stocks.ExistingByProductId.Url + request.Id))
        {
            return Results.Conflict("Product has stock");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        await _cache.Remove(PRODUCTS);
        await _cache.Remove(PRODUCT + request.Id);
        return Results.Ok();
    }
}

