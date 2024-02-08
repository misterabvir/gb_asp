using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Cache;
using StoreApplication.Stores.Errors;
using StoreApplication.Stores.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;

namespace StoreApplication.Stores.Commands.UpdateIdentityNumber;

public sealed class StoresUpdateIdentityNumberCommandHandler : ICommandHandler<StoresUpdateIdentityNumberCommand, Result<StoreResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public StoresUpdateIdentityNumberCommandHandler(StoreContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<StoreResultResponse, Error>> Handle(StoresUpdateIdentityNumberCommand request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(c => c.IdentityNumber == request.IdentityNumber, cancellationToken);
        if (store is not null)
        {
            return StoreErrors.AlreadyExist(request.IdentityNumber);
        }

        store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (store is null)
        {
            return StoreErrors.NotFound(request.Id);
        }

        store.IdentityNumber = request.IdentityNumber;
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(StoreKeys.Id(request.Id), cancellationToken);
        await _cache.RemoveAsync(StoreKeys.All(), cancellationToken);

        return _mapper.Map<StoreResultResponse>(store)!;

    }
}
