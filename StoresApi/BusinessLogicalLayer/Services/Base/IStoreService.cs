using Contracts.Stores.Requests;
using Contracts.Stores.Responses;

namespace StoresApi.BusinessLogicalLayer.Services.Base;

public interface IStoreService
{
    Task<IEnumerable<StoreResponse>> GetStores();
    Task<StoreResponse> GetStoreById(StoreGetByIdRequest request);
    Task<Guid> CreateStore(StoreCreateRequest request);
    Task<bool> UpdateStore(StoreUpdateNameRequest request);
    Task<bool> DeleteStore(StoreDeleteRequest request);
}
