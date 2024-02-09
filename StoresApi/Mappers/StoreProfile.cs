using AutoMapper;
using Contracts.Stores.Requests;
using Contracts.Stores.Responses;
using StoresApi.DataAccessLayer.Models;

namespace StoresApi.Mappers;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreResponse>(MemberList.Destination);
        CreateMap<StoreCreateRequest, Store>(MemberList.Source);
    }
}
