using Domain;
using Persistence.Base;

namespace Persistence.Repositories.Abstractions;

public interface ICategoryRepository : IQueryRepository<Category>, ICommandRepository<Category>
{
    Task<Result<Category, Error>> UpdateName(int id, string name);
}
