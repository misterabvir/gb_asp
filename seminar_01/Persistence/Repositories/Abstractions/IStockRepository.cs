using Domain;
using Persistence.Base;

namespace Persistence.Repositories.Abstractions;

public interface IStockRepository : IRepository<Stock>
{
    Task<Result<Stock, Error>> ImportProduct(int productId, int storeId, int count);
    Task<Result<Stock, Error>> ExportProduct(int productId, int storeId, int count);
    Task<Result<IEnumerable<Stock>, Error>> RelocateProduct(int productId, int storeFromId, int storeToId, int count);
}
