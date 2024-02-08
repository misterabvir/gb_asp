using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stores.Cache;
using StoreApplication.Stores.Errors;
using StoreApplication.Stores.Responses;
using AutoMapper;
using StoreDomain;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;

namespace StoreApplication.Stores.Commands.Create;

public sealed class StoresCreateCommandHandler : ICommandHandler<StoresCreateCommand, Result<StoreResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public StoresCreateCommandHandler(StoreContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<StoreResultResponse, Error>> Handle(StoresCreateCommand request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(c => c.IdentityNumber == request.IdentityNumber, cancellationToken: cancellationToken);
        if (store is not null)
        {
            return StoreErrors.AlreadyExist(request.IdentityNumber);
        }

        store = _mapper.Map<Store>(request)!;

        await _context.Stores.AddAsync(store, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(StoreKeys.All(), cancellationToken);

        return _mapper.Map<StoreResultResponse>(store)!;

    }
}


