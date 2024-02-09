using AutoMapper;
using Contracts.Stores.Requests;
using Contracts.Stores.Responses;
using ExternalLinks;
using ExternalLinks.Base;
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
    private readonly IHttpClientService _query;
    private readonly IMapper _mapper;

    public StoreService(ICacheService cache, StoresContext context, IMapper mapper, IHttpClientService query)
    {
        _cache = cache;
        _context = context;
        _mapper = mapper;
        _query = query;
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

    public async Task<bool> IsExistStoreById(StoreIsExistByIdRequest request)
    {
        StoreResponse? response = await _cache.Get<StoreResponse>(STORE + request.Id);
        if (response is not null)
        {
            return true;
        }
        var store = await _context.Stores.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
        return store is not null;
    }

    public async Task<IResult> CreateStore(StoreCreateRequest request)
    {
        var stores = _mapper.Map<Store>(request);
        await _context.Stores.AddAsync(stores);
        await _context.SaveChangesAsync();
        await _cache.Remove(STORES);
        return Results.Ok(stores.Id);
    }

    public async Task<IResult> UpdateStore(StoreUpdateNameRequest request)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (store is null)
        {
            return Results.NotFound($"Not found store with id: {request.Id}!"); 
        }

        store.Name = request.Name;
        await _context.SaveChangesAsync();
        await _cache.Remove(STORES);
        await _cache.Remove(STORE + request.Id);
        return Results.Ok();
    }
    public async Task<IResult> DeleteStore(StoreDeleteRequest request)
    {
        if (await _query.Get<bool>(Linker.Base.Stocks.ExistingByStoreId.Url + request.Id))
        {
            return Results.BadRequest("Can't delete store with stock!");
        }

        var store = await _context.Stores.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (store is null)
        {
            return Results.NotFound($"Not found store with id: {request.Id}!");
        }
        _context.Remove(store);
        await _context.SaveChangesAsync();
        await _cache.Remove(STORES);
        await _cache.Remove(STORE + request.Id);
        return Results.Ok();
    }
}
