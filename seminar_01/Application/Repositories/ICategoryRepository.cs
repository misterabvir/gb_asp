using Domain;
using Domain.Base;

namespace Application.Repositories;

public interface ICategoryRepository : IQueryRepository<Category>, ICommandRepository<Category>
{
    Task<Result<Category, Error>> UpdateName(Guid id, string name);
}
