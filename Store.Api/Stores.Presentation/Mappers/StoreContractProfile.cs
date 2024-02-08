using StoreApplication.Stores.Commands.Create;
using StoreApplication.Stores.Commands.Delete;
using StoreApplication.Stores.Commands.UpdateIdentityNumber;
using StoreApplication.Stores.Queries.GetById;
using StoreApplication.Stores.Responses;
using AutoMapper;
using StoreContracts.Stores;

namespace StorePresentation.Mappers;

public class StoreContractProfile : Profile
{
    public StoreContractProfile()
    {
        CreateMap<StoreResultResponse, StoreResponse>(MemberList.Destination);
        CreateMap<StoreByIdRequest, StoresGetByIdQuery>(MemberList.Destination);
        CreateMap<StoreCreateRequest, StoresCreateCommand>(MemberList.Destination);
        CreateMap<StoreDeleteRequest, StoresDeleteCommand>(MemberList.Destination);
        CreateMap<StoreUpdateIdentityNumberRequest, StoresUpdateIdentityNumberCommand>(MemberList.Destination);
    }
}
