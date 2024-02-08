using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Responses;

namespace ProductApplication.Categories.Commands.Delete;

public sealed record CategoriesDeleteCommand(Guid Id) :ICommand<Result<CategoryResultResponse, Error>>;

