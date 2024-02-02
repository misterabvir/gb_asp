using Domain;
using Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;
using Persistence.Errors;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class StockRepository : IStockRepository
{
    private readonly StoreContext _context;
    private readonly ILogger _logger;

    public StockRepository(StoreContext context, ILogger<StockRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Stock, Error>> ImportProduct(int productId, int storeId, int quantity)
    {
        try
        {
            if (quantity <= 0)
            {
                return new Problem("Stocks.Repository.ImportProduct", "Count must be greater than 0");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                _logger.LogError($"Product with id {productId} not found");
                return new NotFound("Stocks.Repository.ImportProduct", $"Product with id {productId} not found");
            }


            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == storeId);
            if (store == null)
            {
                _logger.LogError($"Store with id {storeId} not found");
                return new NotFound("Stocks.Repository.ImportProduct", $"Store with id {storeId} not found");
            }

            var stock = await _context.StoreProducts.FirstOrDefaultAsync(sp => sp.ProductId == productId && sp.StoreId == storeId);
            if (stock is null)
            {
                stock = new Stock()
                {
                    ProductId = productId,
                    StoreId = storeId
                };

                stock = (await _context.StoreProducts.AddAsync(stock)).Entity;
                _logger.LogInformation("Product added to store");
            }
            else
            {
                stock.Quantity += quantity;
                _logger.LogInformation("Product qantity updated in store"); 
            }

            
            return await _context
                .StoreProducts
                .Include(x => x.Product)
                .Include(x => x.Store)
                .AsNoTracking()
                .FirstAsync(sp => sp.ProductId == stock.ProductId && sp.StoreId == stock.ProductId); 
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.GetById", exception.Message);
        }
    }

    public async Task<Result<Stock, Error>> ExportProduct(int productId, int storeId, int count)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                _logger.LogError($"Product with id {productId} not found");
                return new NotFound("Stocks.Repository.ExportProduct", $"Product with id {productId} not found");
            }

            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == storeId);
            if (store == null)
            {
                _logger.LogError($"Store with id {storeId} not found");
                return new NotFound("Stocks.Repository.ExportProduct", $"Store with id {storeId} not found");
            }
            var stock = await _context
                .StoreProducts
                .Include(x=>x.Product)
                .Include(x=>x.Store)
                .FirstOrDefaultAsync(sp => sp.ProductId == productId && sp.StoreId == storeId);
            
            if(stock is null)
            {
                _logger.LogError($"Stock for product  with id {productId} not found in store with id {storeId}");
                return new NotFound("Stocks.Repository.ExportProduct", $"Stock for product with id {productId} not found in store with id {storeId}");
            }
            
            if(stock.Quantity < count)
            {
                _logger.LogError($"Not enough products in store");
                return new Problem("Stocks.Repository.ExportProduct", "Not enough products in store");
            }

            stock.Quantity -= count;

            if(stock.Quantity == 0)
            {
                _context.StoreProducts.Remove(stock);
            }

            _logger.LogInformation("Product removed from store");
            return await _context
                .StoreProducts
                .Include(x => x.Product)
                .Include(x => x.Store)
                .AsNoTracking()
                .FirstAsync(sp => sp.ProductId == stock.ProductId && sp.StoreId == stock.ProductId); ;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.GetById", exception.Message);
        }
    }

    public async Task<Result<IEnumerable<Stock>, Error>> RelocateProduct(int productId, int fromStoreId, int toStoreId, int count)
    {
        try
        {
            var stockResultRemove = await ExportProduct(productId, fromStoreId, count);
            if (stockResultRemove.IsFailure)
            {
                return stockResultRemove.Errors.ToList();
            }

            var stockResultAdd = await ImportProduct(productId, toStoreId, count);
            if (stockResultAdd.IsFailure)
            {
                return stockResultAdd.Errors.ToList();
            }

            return new List<Stock>() { stockResultRemove.Value!, stockResultAdd.Value! };

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.GetById", exception.Message);
        }
    }
}
