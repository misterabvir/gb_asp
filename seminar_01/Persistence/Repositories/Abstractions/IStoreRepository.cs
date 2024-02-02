using Domain;
using Persistence.Base;

namespace Persistence.Repositories.Abstractions;

public interface IStoreRepository : IQueryRepository<Store>, ICommandRepository<Store>
{
    Task<Result<Store, Error>> UpdateIdentityNumber(int id, string identityNumber);
}
