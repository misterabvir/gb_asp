using Contracts.Stores.Requests;
using Contracts.Stores.Responses;

namespace StoresApi.BusinessLogicalLayer.Services.Base;

public interface IStoreService
{
    Task<IEnumerable<StoreResponse>> GetStores();
    Task<StoreResponse> GetStoreById(StoreGetByIdRequest request);
    Task<bool> IsExistStoreById(StoreIsExistByIdRequest request);
    Task<IResult> CreateStore(StoreCreateRequest request);
    Task<IResult> UpdateStore(StoreUpdateNameRequest request);
    Task<IResult> DeleteStore(StoreDeleteRequest request);
}
