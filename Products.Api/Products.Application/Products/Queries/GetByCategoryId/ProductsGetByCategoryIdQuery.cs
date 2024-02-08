using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Queries.GetByCategoryId;

public record ProductsGetByCategoryIdQuery(Guid? CategoryId) : IQuery<Result<IEnumerable<ProductResultResponse>, Error>>;
