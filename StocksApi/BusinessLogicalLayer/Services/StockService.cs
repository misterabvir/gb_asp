using AutoMapper;
using Contracts.Stocks.Requests;
using Contracts.Stocks.Responses;
using Microsoft.EntityFrameworkCore;
using StocksApi.BusinessLogicalLayer.Services.Base;
using StocksApi.DataAccessLayer.Contexts;
using StocksApi.DataAccessLayer.Models;

namespace StocksApi.BusinessLogicalLayer.Services;

public class StockService : IStockService
{
    private const string ProductStocks = "product_stocks";
    private const string StoreStocks = "store_stocks";
    private readonly ICacheService _cache;
    private readonly StocksContext _context;
    private readonly IExternalQueryService _queryService;
    private readonly IMapper _mapper;

    public StockService(ICacheService cache, StocksContext context, IExternalQueryService queryService, IMapper mapper)
    {
        _cache = cache;
        _context = context;
        _queryService = queryService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StockResponse>> GetStocksByProductId(StockGetByProductIdRequest request)
    {
        var productExist = await _queryService.IsProductExist(request.ProductId);
        if (!productExist)
        {
            return [];
        }

        var stocks = await _context.Stocks.Where(x => x.ProductId == request.ProductId).AsNoTracking().ToListAsync();
        var response = stocks.Select(_mapper.Map<StockResponse>).ToList();
        return response;
    }

    public async Task<IEnumerable<StockResponse>> GetStocksByStoreId(StockGetByStoreIdRequest request)
    {
        if (!await _queryService.IsStoreExist(request.StoreId))
        {
            return [];
        }
        var stocks = await _context.Stocks.Where(x => x.StoreId == request.StoreId).AsNoTracking().ToListAsync();
        var response = stocks.Select(_mapper.Map<StockResponse>).ToList();
        await _cache.Set(StoreStocks, response);

        return response;
    }

    public async Task<bool> ImportToStore(StockImportToStoreRequest request)
    {
        if (!await _queryService.IsProductExist(request.ProductId))
        {
            return false;
        }

        if (!await _queryService.IsProductExist(request.ProductId))
        {
            return false;
        }
        if (request.Quantity < 0)
        {
            return false;
        }
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.StoreId == request.StoreId);
        if (stock is null)
        {
            stock = new Stock() { ProductId = request.ProductId, StoreId = request.StoreId, Quantity = 0 };
            await _context.Stocks.AddAsync(stock);
        }
        stock.Quantity += request.Quantity;
        await _context.SaveChangesAsync();

        await _cache.Remove(ProductStocks);
        await _cache.Remove(StoreStocks);

        return true;
    }

    public async Task<bool> ExportFromStore(StockExportFromStoreRequest request)
    {
        if (!await _queryService.IsProductExist(request.ProductId)
            || !await _queryService.IsStoreExist(request.StoreId)
            || request.Quantity < 0)
        {
            return false;
        }

        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.StoreId == request.StoreId);
        if (stock is null || stock.Quantity < request.Quantity)
        {
            return false;
        }
        stock.Quantity -= request.Quantity;
        await _context.SaveChangesAsync();

        await _cache.Remove(ProductStocks);
        await _cache.Remove(StoreStocks);

        return true;
    }

    public async Task<bool> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request)
    {
        if (!await _queryService.IsProductExist(request.ProductId)
            || !await _queryService.IsStoreExist(request.StoreSenderId)
            || !await _queryService.IsStoreExist(request.StoreTargetId)
            || request.Quantity < 0)
        {
            return false;
        }

        var stockSender = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.StoreId == request.StoreSenderId);
        if (stockSender is null || stockSender.Quantity < request.Quantity)
        {
            return false;
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

        await _cache.Remove(ProductStocks);
        await _cache.Remove(StoreStocks);

        return true;
    }
}