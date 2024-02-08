using ProductApplication.Products.Commands.Create;
using ProductApplication.Products.Commands.Delete;
using ProductApplication.Products.Commands.UpdateCategory;
using ProductApplication.Products.Commands.UpdateDescription;
using ProductApplication.Products.Commands.UpdateName;
using ProductApplication.Products.Commands.UpdatePrice;
using ProductApplication.Products.Queries.GetByCategoryId;
using ProductApplication.Products.Responses;
using AutoMapper;
using ProductApplication.Products.Queries.GetById;
using ProductContracts.Products;


namespace ProductPresentation.Mappers;

public class ProductContractProfile : Profile 
{
    public ProductContractProfile()
    {
        CreateMap<ProductResultResponse, ProductResponse>(MemberList.Destination);

        CreateMap<ProductGetByIdRequest, ProductsGetByIdQuery>(MemberList.Destination).ForMember(s=>s.Id, dest=>dest.MapFrom(c=>c.Id));
        CreateMap<ProductGetByCategoryIdRequest, ProductsGetByCategoryIdQuery>(MemberList.Destination);

        CreateMap<ProductCreateRequest, ProductsCreateCommand>(MemberList.Destination);
        CreateMap<ProductUpdateNameRequest, ProductsUpdateNameCommand>(MemberList.Destination);
        CreateMap<ProductUpdatePriceRequest, ProductsUpdatePriceCommand>(MemberList.Destination);
        CreateMap<ProductUpdateDescriptionRequest, ProductsUpdateDescriptionCommand>(MemberList.Destination);
        CreateMap<ProductUpdateCategoryRequest, ProductsUpdateCategoryCommand>(MemberList.Destination);
        CreateMap<ProductDeleteRequest, ProductsDeleteCommand>(MemberList.Destination);
    }
}
