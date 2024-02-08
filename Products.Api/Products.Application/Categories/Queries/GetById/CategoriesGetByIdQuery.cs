using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Responses;

namespace ProductApplication.Categories.Queries.GetById;

public sealed record CategoriesGetByIdQuery(Guid Id) : IQuery<Result<CategoryResultResponse, Error>>;
