using StoreApplication.Stocks.Commands.ImportToStore;
using StoreApplication.Stocks.Responses;
using AutoMapper;
using StoreDomain;

namespace StoreApplication.Stocks.Mappers;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockResultResponse>(MemberList.Destination);
        CreateMap<StockImportToStoreCommand, Stock>(MemberList.Destination);

    }
}
