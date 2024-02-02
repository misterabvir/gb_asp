using Domain.Base;

namespace Application.Repositories;

public interface IQueryRepository<Entity> : IRepository<Entity> 
{
    Task<Result<IEnumerable<Entity>, Error>> Get();
    Task<Result<Entity, Error>> Get(Guid id);
}
