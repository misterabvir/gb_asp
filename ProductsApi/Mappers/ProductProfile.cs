using AutoMapper;
using Contracts.Products.Requests;
using Contracts.Products.Responses;
using ProductsApi.DataAccessLayer.Models;

namespace ProductsApi.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>(MemberList.Destination);
        CreateMap<ProductCreateRequest, Product>(MemberList.Source);
    }
}
