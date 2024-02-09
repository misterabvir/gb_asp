using AutoMapper;
using Contracts.Stores.Requests;
using Contracts.Stores.Responses;
using Microsoft.EntityFrameworkCore;
using StoresApi.BusinessLogicalLayer.Services.Base;
using StoresApi.DataAccessLayer.Contexts;
using StoresApi.DataAccessLayer.Models;

namespace StoresApi.BusinessLogicalLayer.Services;

public class StoreService : IStoreService
{
    private const string STORE = "store_";
    private const string STORES = "stores";
    private readonly ICacheService _cache;
    private readonly StoresContext _context;
    private readonly IExternalQueryService _queryService;
    private readonly IMapper _mapper;

    public StoreService(ICacheService cache, StoresContext context, IMapper mapper, IExternalQueryService queryService)
    {
        _cache = cache;
        _context = context;
        _mapper = mapper;
        _queryService = queryService;
    }
    public async Task<IEnumerable<StoreResponse>> GetStores()
    {
        IEnumerable<StoreResponse>? response = await _cache.Get<IEnumerable<StoreResponse>>(STORES);
        if (response is null)
        {
            var categories = await _context.Stores.AsNoTracking().ToListAsync();
            response = categories.Select(_mapper.Map<StoreResponse>);
            await _cache.Set(STORES, response);
        }
        return response;
    }
    public async Task<StoreResponse> GetStoreById(StoreGetByIdRequest request)
    {
        StoreResponse? response = await _cache.Get<StoreResponse>(STORE + request.Id);
        if (response is not null)
        {
            return response;
        }

        var categories = await _context.Stores.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
        response = _mapper.Map<StoreResponse>(categories);

        await _cache.Set(STORE + request.Id, response);

        return response;
    }

    public async Task<Guid> CreateStore(StoreCreateRequest request)
    {
        var stores = _mapper.Map<Store>(request);
        await _context.Stores.AddAsync(stores);
        await _context.SaveChangesAsync();
        await _cache.Remove(STORES);
        return stores.Id;
    }

    public async Task<bool> UpdateStore(StoreUpdateNameRequest request)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (store is null)
        {
            return false;
        }

        store.Name = request.Name;
        await _context.SaveChangesAsync();
        await _cache.Remove(STORES);
        await _cache.Remove(STORE + request.Id);
        return true;
    }
    public async Task<bool> DeleteStore(StoreDeleteRequest request)
    {
        if (await _queryService.IsStockExist(request.Id))
        {
            return false;
        }

        var store = await _context.Stores.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (store is null)
        {
            return false;
        }
        _context.Remove(store);
        await _context.SaveChangesAsync();
        await _cache.Remove(STORES);
        await _cache.Remove(STORE + request.Id);
        return true;
    }
}
