using Domain;
using Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;
using Persistence.Errors;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly StoreContext _context;
    private readonly ILogger _logger;

    public StoreRepository(StoreContext context, ILogger<StoreRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Store>, Error>> Get()
    {
        try
        {
            var categories = await _context
                .Stores
                .Include(x => x.Stocks ?? new())
                .ThenInclude(x=>x.Product)
                .AsNoTracking()
                .ToListAsync();
            _logger.LogInformation("Stores get all"); 
            return categories;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Store.Repository.GetAll", exception.Message);
        }
    }

    public async Task<Result<Store, Error>> Get(int id)
    {
        try
        {
            var store = await _context
                .Stores
                .Include(x => x.Stocks ?? new())
                .ThenInclude(x => x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if(store is null)
            {
                _logger.LogInformation("Store not found");
                return new NotFound("Store.Repository.GetById", "Store not found");
            }
            _logger.LogInformation($"Store \"{store.IdentityNumber}\" get");
            return store;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Store.Repository.GetById", exception.Message);
        }
    }

    public async Task<Result<Store, Error>> Create(Store entity)
    {
        try
        {
            var store = await _context.Stores.FirstOrDefaultAsync(c => c.IdentityNumber == entity.IdentityNumber);
            if (store is not null)
            {
                _logger.LogWarning("Store already exists");
                return new AlreadyExist("Store.Repository.Create", $"Store with name {entity.IdentityNumber} already exists");
            }

            store = (await _context.Stores.AddAsync(entity)).Entity;

            _logger.LogInformation($"Store \"{store.IdentityNumber}\" added");
            return store;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Store.Repository.Create", exception.Message);
        }
    }

    public async Task<Result<Store, Error>> UpdateIdentityNumber(int id, string identityNumber)
    {
        try
        {
            var store = await _context.Stores.FirstOrDefaultAsync(c => c.IdentityNumber == identityNumber);
            if (store is not null)
            {
                _logger.LogWarning("Store already exists");
                return new AlreadyExist("Store.Repository.UpdateIdentityNumber", $"Store with name {identityNumber} already exists");
            }

            store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);
            if (store is null)
            {
                _logger.LogInformation("Store not found");
                return new NotFound("Store.Repository.UpdateIdentityNumber", "Store not found");
            }

            store.IdentityNumber = identityNumber;

            _logger.LogInformation($"Store \"{store.IdentityNumber}\" get");
            return store;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Store.Repository.UpdateIdentityNumber", exception.Message);
        }
    }

    public async Task<Result<Store, Error>> Delete(int id)
    {
        try
        {
            var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);
            if (store is null)
            {
                _logger.LogInformation("Store not found");
                return new NotFound("Store.Repository.Delete", "Store not found");
            }

            _context.Stores.Remove(store);

            _logger.LogInformation($"Store \"{store.IdentityNumber}\" deleted");
            return store;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Store.Repository.Delete", exception.Message);
        }
    }
}