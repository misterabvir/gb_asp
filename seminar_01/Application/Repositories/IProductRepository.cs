using Domain;
using Domain.Base;

namespace Application.Repositories;

public interface IProductRepository : IQueryRepository<Product>, ICommandRepository<Product>
{
    Task<Result<Product, Error>> UpdateName(Guid id, string name);
    Task<Result<Product, Error>> UpdateDescription(Guid id, string description);
    Task<Result<Product, Error>> UpdatePrice(Guid id, decimal price);
    Task<Result<Product, Error>> UpdateCategory(Guid id, Guid? categoryId);
    Task<Result<IEnumerable<Product>, Error>> GetByCategory(Guid? categoryId);

}
