using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Queries.GetByCategoryId;

public record ProductsGetByCategoryIdQuery(int? CategoryId) : IQuery<Result<IEnumerable<ProductResultResponse>, Error>>;
