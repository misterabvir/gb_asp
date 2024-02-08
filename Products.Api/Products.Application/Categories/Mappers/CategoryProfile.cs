using ProductApplication.Categories.Commands.Create;
using ProductApplication.Categories.Responses;
using AutoMapper;
using Domain;

namespace ProductApplication.Categories.Mappers;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResultResponse>();
        CreateMap<CategoriesCreateCommand, Category>();
    }
}
