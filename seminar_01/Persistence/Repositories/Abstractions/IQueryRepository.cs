using Domain.Base;
using Persistence.Base;

namespace Persistence.Repositories.Abstractions;

public interface IQueryRepository<TEntity> : IRepository<TEntity> where TEntity :Entity
{
    Task<Result<IEnumerable<TEntity>, Error>> Get();
    Task<Result<TEntity, Error>> Get(int id);
}
