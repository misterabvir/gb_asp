namespace ProductsApi.BusinessLogicalLayer.Services.Base;

public interface IExternalQueryService
{
    Task<bool> IsStockExist(Guid Id);
}
