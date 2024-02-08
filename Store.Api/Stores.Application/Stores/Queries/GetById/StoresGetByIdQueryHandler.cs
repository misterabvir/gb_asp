using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Cache;
using StoreApplication.Stores.Errors;
using StoreApplication.Stores.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;

namespace StoreApplication.Stores.Queries.GetById;

public sealed class StoresGetByIdQueryHandler : IQueryHandler<StoresGetByIdQuery, Result<StoreResultResponse, Error>>
{

    private readonly StoreContext _context;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public StoresGetByIdQueryHandler(StoreContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Result<StoreResultResponse, Error>> Handle(StoresGetByIdQuery request, CancellationToken cancellationToken)
    {

        StoreResultResponse? response = await _cache.GetAsync<StoreResultResponse>(StoreKeys.Id(request.Id), cancellationToken);
        if (response is not null)
        {
            return response;
        }

        var store = await _context.Stores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (store is null)
        {
            return StoreErrors.NotFound(request.Id);
        }

        response = _mapper.Map<StoreResultResponse>(store)!;
        await _cache.SetAsync(StoreKeys.Id(request.Id), response, cancellationToken);

        return response;
    }
}
