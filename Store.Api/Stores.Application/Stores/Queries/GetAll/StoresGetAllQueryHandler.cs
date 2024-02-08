using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Cache;
using StoreApplication.Stores.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;

namespace StoreApplication.Stores.Queries.GetAll;

public sealed class StoresGetAllQueryHandler : IQueryHandler<StoresGetAllQuery, Result<IEnumerable<StoreResultResponse>, Error>>
{
    private readonly StoreContext _context;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public StoresGetAllQueryHandler(StoreContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<StoreResultResponse>, Error>> Handle(StoresGetAllQuery _, CancellationToken cancellationToken)
    {
        List<StoreResultResponse>? response = await _cache.GetAsync<List<StoreResultResponse>>(StoreKeys.All(), cancellationToken);
        if (response is not null)
        {
            return response;
        }

        var stores = await _context.Stores.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

        response = stores.Select(_mapper.Map<StoreResultResponse>).ToList()!;
        await _cache.SetAsync(StoreKeys.All(), response, cancellationToken);

        return response;
    }
}
