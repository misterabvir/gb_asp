using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Products.Responses;

namespace ProductApplication.Products.Queries.GetById;

public record ProductsGetByIdQuery(Guid Id) : IQuery<Result<ProductResultResponse, Error>>;
