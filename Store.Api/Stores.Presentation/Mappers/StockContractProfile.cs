using StoreApplication.Stocks.Commands.ExportFromStore;
using StoreApplication.Stocks.Commands.ImportToStore;
using StoreApplication.Stocks.Commands.MoveBetweenStores;
using StoreApplication.Stocks.Queries.GetByProductId;
using StoreApplication.Stocks.Queries.GetByStoreId;
using StoreApplication.Stocks.Responses;
using AutoMapper;
using StoreContracts.Stocks;

namespace StorePresentation.Mappers;

public class StockContractProfile : Profile
{
    public StockContractProfile()
    {
        CreateMap<StockResultResponse, StockResponse>(MemberList.Destination);

        CreateMap<StockByProductIdRequest, StockGetByProductIdQuery>(MemberList.Destination);
        CreateMap<StockByStoreIdRequest, StockGetByStoreIdQuery>(MemberList.Destination);

        CreateMap<StockImportRequest, StockImportToStoreCommand>(MemberList.Destination);
        CreateMap<StockExportRequest, StockExportFromStoreCommand>(MemberList.Destination);
        CreateMap<StockMoveRequest, StockMoveBetweenStoresCommand>(MemberList.Destination);
    }
}