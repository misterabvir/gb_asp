using StoreApplication.Stores.Commands.Create;
using StoreApplication.Stores.Responses;
using AutoMapper;
using StoreDomain;

namespace StoreApplication.Stores.Mappers;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreResultResponse>(MemberList.Destination);
        CreateMap<StoresCreateCommand, Store>(MemberList.Destination);

    }
}
