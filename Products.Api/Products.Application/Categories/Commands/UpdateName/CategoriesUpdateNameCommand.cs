using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Responses;

namespace ProductApplication.Categories.Commands.UpdateName;

public sealed record CategoriesUpdateNameCommand(Guid Id, string Name) : ICommand<Result<CategoryResultResponse, Error>>;
