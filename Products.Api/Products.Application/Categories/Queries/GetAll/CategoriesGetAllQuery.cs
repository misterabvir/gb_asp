using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Responses;
using ProductApplication.Products.Queries.GetById;

namespace ProductApplication.Categories.Queries.GetAll;

public record CategoriesGetAllQuery() : IQuery<Result<IEnumerable<CategoryResultResponse>, Error>>;
