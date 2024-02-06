using Application.Abstractions;
using Application.Base;
using Application.Categories.Responses;

namespace Application.Categories.Commands.Delete;

public sealed record CategoriesDeleteCommand(int Id) :ICommand<Result<CategoryResultResponse, Error>>;

