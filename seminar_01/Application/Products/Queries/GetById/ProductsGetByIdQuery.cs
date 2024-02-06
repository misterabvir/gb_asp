using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Queries.GetById;

public record ProductsGetByIdQuery(int Id) : IQuery<Result<ProductResultResponse, Error>>;
