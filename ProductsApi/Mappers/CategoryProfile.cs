using AutoMapper;
using Contracts.Categories.Requests;
using Contracts.Categories.Responses;
using ProductsApi.DataAccessLayer.Models;

namespace ProductsApi.Mappers;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponse>(MemberList.Destination);
        CreateMap<CategoryCreateRequest, Category>(MemberList.Source);
    }
}