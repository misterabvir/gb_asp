using Domain.Base;
using Persistence.Base;

namespace Persistence.Repositories.Abstractions;

public interface ICommandRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    Task<Result<TEntity, Error>> Create(TEntity entity);
    Task<Result<TEntity, Error>> Delete(int id);
}
