using Domain;
using Domain.Base;

namespace Application.Repositories;

public interface IStockRepository : IRepository<Stock>
{
    Task<Result<Stock, Error>> AddProductInStore(Guid productId, Guid storeId, int count);
    Task<Result<Stock, Error>> RemoveProductInSrore(Guid productId, Guid storeId, int count);
    Task<Result<IEnumerable<Stock>, Error>> MoveProductInAnotherSrore(Guid productId, Guid storeFromId, Guid storeToId, int count);
}
