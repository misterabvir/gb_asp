using AutoMapper;
using Contracts.Stocks.Responses;
using StocksApi.DataAccessLayer.Models;

namespace StocksApi.Mappers;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockResponse>(MemberList.Destination);
    }
}