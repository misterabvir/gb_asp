using ProductApplication.Products.Commands.Create;
using ProductApplication.Products.Responses;
using AutoMapper;
using Domain;

namespace ProductApplication.Products.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResultResponse>(MemberList.Destination);
        CreateMap<ProductsCreateCommand, Product>(MemberList.Destination);
    }
}
