using Domain;
using Domain.Base;

namespace Application.Repositories;

public interface IStoreRepository : IQueryRepository<Store>,ICommandRepository<Store>
{
    Task<Result<Store,Error>> UpdateIdentityNumber(Guid id, string identityNumber);
}
