using Domain.Base;
using Persistence.Base;

namespace Persistence.Repositories.Abstractions;

public interface IUnitOfWork
{
    Task<Result<int, Error>> SaveChangesAsync();
}
