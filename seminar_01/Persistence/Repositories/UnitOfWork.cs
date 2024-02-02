using Domain.Base;
using Microsoft.Extensions.Logging;
using Persistence.Base;
using Persistence.Contexts;
using Persistence.Errors;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _context;
    private readonly ILogger _logger;

    public UnitOfWork(StoreContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<int, Error>> SaveChangesAsync()
    {
        try
        {
            var result = await _context.SaveChangesAsync();
            _logger.LogInformation($"Changes ({result}) saved to database  ");
            return result;
        }
        catch (Exception exception)
        {

            return new Problem("UnitOfWork.SaveChangesAsync", exception.Message);
        }      
    }
}
   
