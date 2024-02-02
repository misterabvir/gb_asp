using Domain;
using Persistence.Base;

namespace Persistence.Repositories.Abstractions;

public interface IProductRepository : IQueryRepository<Product>, ICommandRepository<Product>
{
    Task<Result<Product, Error>> UpdateName(int id, string name);
    Task<Result<Product, Error>> UpdateDescription(int id, string description);
    Task<Result<Product, Error>> UpdatePrice(int id, decimal price);
    Task<Result<Product, Error>> UpdateCategory(int id, int? categoryId);
    Task<Result<IEnumerable<Product>, Error>> GetByCategory(int? categoryId);

}
