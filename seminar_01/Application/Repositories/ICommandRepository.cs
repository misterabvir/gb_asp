using Domain.Base;

namespace Application.Repositories;

public interface ICommandRepository<Entity> : IRepository<Entity>
{
    Task<Result<Entity, Error>> Create(Entity entity);
    Task<Result<Entity, Error>> Delete(Guid id);
}
