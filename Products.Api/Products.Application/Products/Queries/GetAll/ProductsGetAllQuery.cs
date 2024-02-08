using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Queries.GetAll;

public record ProductsGetAllQuery() : IQuery<Result<IEnumerable<ProductResultResponse>, Error>>;
