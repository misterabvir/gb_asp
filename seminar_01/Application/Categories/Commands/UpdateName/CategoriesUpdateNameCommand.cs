using Application.Abstractions;
using Application.Base;
using Application.Categories.Queries.GetAll;
using Application.Categories.Responses;

namespace Application.Categories.Commands.UpdateName;

public sealed record CategoriesUpdateNameCommand(int Id, string Name) : ICommand<Result<CategoryResultResponse, Error>>;
