using ProductApplication.Categories.Commands.Create;
using ProductApplication.Categories.Commands.Delete;
using ProductApplication.Categories.Commands.UpdateName;
using ProductApplication.Categories.Queries.GetById;
using ProductApplication.Categories.Responses;
using AutoMapper;
using ProductContracts.Categories;

namespace ProductPresentation.Mappers;

public class CategoryContractProfile : Profile
{
    public CategoryContractProfile()
    {
        CreateMap<CategoryResultResponse, CategoryResponse>(MemberList.Destination);

        CreateMap<CategoryGetByIdRequest, CategoriesGetByIdQuery>(MemberList.Destination);

        CreateMap<CategoryCreateRequest, CategoriesCreateCommand>(MemberList.Destination);
        CreateMap<CategoryUpdateNameRequest, CategoriesUpdateNameCommand>(MemberList.Destination);
        CreateMap<CategoryDeleteRequest, CategoriesDeleteCommand>(MemberList.Destination);
    }
}