using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Responses;
using Domain.Base;

namespace ProductApplication.Categories.Commands.Create;

public sealed record CategoriesCreateCommand(string Name) : ICommand<Result<CategoryResultResponse, Error>>;
