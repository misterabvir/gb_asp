namespace StocksApi.BusinessLogicalLayer.Services.Base;

public interface IExternalQueryService
{
    Task<bool> IsProductExist(Guid Id);
    Task<bool> IsStoreExist(Guid Id);
}
