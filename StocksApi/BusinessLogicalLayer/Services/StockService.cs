using AutoMapper;
using Contracts.Stocks.Requests;
using Contracts.Stocks.Responses;
using ExternalLinks;
using ExternalLinks.Base;
using Microsoft.EntityFrameworkCore;
using StocksApi.BusinessLogicalLayer.Services.Base;
using StocksApi.DataAccessLayer.Contexts;
using StocksApi.DataAccessLayer.Models;

namespace StocksApi.BusinessLogicalLayer.Services;

public class StockService : IStockService
{
    private const string PRODUCTSTOCKS = "product_stocks";
    private const string STORESTOCKS = "store_stocks";
    private readonly ICacheService _cache;
    private readonly StocksContext _context;
    private readonly IHttpClientService _query;
    private readonly IMapper _mapper;

    public StockService(ICacheService cache, StocksContext context, IHttpClientService query, IMapper mapper)
    {
        _cache = cache;
        _context = context;
        _query = query;
        _mapper = mapper;
    }

    public async Task<bool> IsExistStocksByProductId(StockIsExistByProductIdRequest request)
    {
        var cached = await _cache.Get<IEnumerable<StockResponse>>(PRODUCTSTOCKS + request.Id);
        if(cached is not null)
        {
            return cached.Any();
        }              
        var stocks = await _context.Stocks.Where(x => x.ProductId == request.Id).AsNoTracking().ToListAsync();
        return stocks.Count != 0;
    }

    public async Task<bool> IsExistStocksByStoreId(StockIsExistByStoreIdRequest request)
    {
        var cached = await _cache.Get<IEnumerable<StockResponse>>(STORESTOCKS + request.Id);
        if (cached is not null)
        {
            return cached.Any();
        }
        var stocks = await _context.Stocks.Where(x => x.StoreId == request.Id).AsNoTracking().ToListAsync();
        return stocks.Count != 0;
    }

    public async Task<IEnumerable<StockResponse>> GetStocksByProductId(StockGetByProductIdRequest request)
    {
        if (!await _query.Get<bool>(Linker.Base.Products.ExistingById.Url + request.Id))
        {
            return [];
        }

        var response = await _cache.Get<IEnumerable<StockResponse>>(PRODUCTSTOCKS + request.Id);
        if(response is null)
        {
            var stocks = await _context.Stocks.Where(x => x.ProductId == request.Id).AsNoTracking().ToListAsync();
            response = stocks.Select(_mapper.Map<StockResponse>).ToList();
            await _cache.Set(PRODUCTSTOCKS + request.Id, response);
        }
        return response;
    }

    public async Task<IEnumerable<StockResponse>> GetStocksByStoreId(StockGetByStoreIdRequest request)
    {
        if (!await _query.Get<bool>(Linker.Base.Stores.ExistingById.Url + request.Id))
        {
            return [];
        }
        var response = await _cache.Get<IEnumerable<StockResponse>>(STORESTOCKS + request.Id);
        if (response is null)
        {
            var stocks = await _context.Stocks.Where(x => x.StoreId == request.Id).AsNoTracking().ToListAsync();
            response = stocks.Select(_mapper.Map<StockResponse>).ToList();
            await _cache.Set(STORESTOCKS, response);
        }
        return response;
    }

    public async Task<IResult> ImportToStore(StockImportToStoreRequest request)
    {
        if (request.Quantity < 0)
        {
            return Results.BadRequest($"The quantity can not be less than zero {request.Quantity}");
        }

        if (!await _query.Get<bool>(Linker.Base.Products.ExistingById.Url + request.ProductId))
        {
            return Results.NotFound($"Not found product with id: {request.ProductId}");
        }

        if (!await _query.Get<bool>(Linker.Base.Stores.ExistingById.Url + request.StoreId))
        {
            return Results.NotFound($"Not found store with id: {request.StoreId}");
        }
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.StoreId == request.StoreId);
        if (stock is null)
        {
            stock = new Stock() { ProductId = request.ProductId, StoreId = request.StoreId, Quantity = 0 };
            await _context.Stocks.AddAsync(stock);
        }
        stock.Quantity += request.Quantity;
        await _context.SaveChangesAsync();

        await _cache.Remove(PRODUCTSTOCKS + request.ProductId);
        await _cache.Remove(STORESTOCKS + request.StoreId);

        return Results.Ok();
    }

    public async Task<IResult> ExportFromStore(StockExportFromStoreRequest request)
    {
        if (request.Quantity < 0)
        {
            return Results.BadRequest($"The quantity can not be less than zero {request.Quantity}");
        }

        if (!await _query.Get<bool>(Linker.Base.Products.ExistingById.Url + request.ProductId))
        {
            return Results.NotFound($"Not found product with id: {request.ProductId}");
        }

        if (!await _query.Get<bool>(Linker.Base.Stores.ExistingById.Url + request.StoreId))
        {
            return Results.NotFound($"Not found store with id: {request.StoreId}");
        }

        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.StoreId == request.StoreId);
        if (stock is null || stock.Quantity < request.Quantity)
        {
            return Results.BadRequest($"Not enough quantity in store. The quantity in store is {stock?.Quantity} and the quantity in exchange is {request.Quantity}");
        }

        stock.Quantity -= request.Quantity;
        await _context.SaveChangesAsync();

        await _cache.Remove(PRODUCTSTOCKS + request.ProductId);
        await _cache.Remove(STORESTOCKS + request.StoreId);

        return Results.Ok();
    }

    public async Task<IResult> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request)
    {
        if (request.Quantity < 0)
        {
            return Results.BadRequest($"The quantity can not be less than zero {request.Quantity}");
        }

        if (!await _query.Get<bool>(Linker.Base.Products.ExistingById.Url + request.ProductId))
        {
            return Results.NotFound($"Not found product with id: {request.ProductId}");
        }

        if (!await _query.Get<bool>(Linker.Base.Stores.ExistingById.Url + request.StoreSenderId))
        {
            return Results.NotFound($"Not found store with id: {request.StoreSenderId}");
        }

        if (!await _query.Get<bool>(Linker.Base.Stores.ExistingById.Url + request.StoreTargetId))
        {
            return Results.NotFound($"Not found store with id: {request.StoreTargetId}");
        }

        var stockSender = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.StoreId == request.StoreSenderId);
        if (stockSender is null)
        {
            return Results.Conflict($"Not found stock with product id: {request.ProductId} and store id: {request.StoreSenderId} in sender store");
        }

        if (stockSender.Quantity < request.Quantity)
        {
            return Results.BadRequest($"Not enough quantity in sender store. The quantity in sender store is {stockSender.Quantity} and the quantity in exchange is {request.Quantity} in exchange store");
        }

        var stockTarget = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.StoreId == request.StoreTargetId);
        if (stockTarget is null)
        {
            stockTarget = new Stock() { ProductId = request.ProductId, StoreId = request.StoreTargetId, Quantity = 0 };
            await _context.Stocks.AddAsync(stockTarget);
        }

        stockSender.Quantity -= request.Quantity;
        stockTarget.Quantity += request.Quantity;

        if (stockSender.Quantity == 0)
        {
            _context.Stocks.Remove(stockSender);
        }

        await _context.SaveChangesAsync();

        await _cache.Remove(PRODUCTSTOCKS + request.ProductId);
        await _cache.Remove(STORESTOCKS + request.StoreSenderId);
        await _cache.Remove(STORESTOCKS + request.StoreTargetId);

        return Results.Ok();
    }
}