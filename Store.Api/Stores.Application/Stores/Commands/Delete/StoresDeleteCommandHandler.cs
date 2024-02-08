using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Cache;
using StoreApplication.Stores.Errors;
using StoreApplication.Stores.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;

namespace StoreApplication.Stores.Commands.Delete;

public sealed class StoresDeleteCommandHandler : ICommandHandler<StoresDeleteCommand, Result<StoreResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public StoresDeleteCommandHandler(StoreContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }


    public async Task<Result<StoreResultResponse, Error>> Handle(StoresDeleteCommand request, CancellationToken cancellationToken)
    {

        var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (store is null)
        {
            return StoreErrors.NotFound(request.Id);
        }

        if(await _context.Stocks.AnyAsync(s=>s.StoreId == request.Id, cancellationToken: cancellationToken))
        {
            return StoreErrors.NotEmpty(store.IdentityNumber);
        }

        _context.Stores.Remove(store);
        await _context.SaveChangesAsync(cancellationToken);
        
        await _cache.RemoveAsync(StoreKeys.Id(request.Id), cancellationToken);
        await _cache.RemoveAsync(StoreKeys.All(), cancellationToken);

        return _mapper.Map<StoreResultResponse>(store)!;

    }
}

