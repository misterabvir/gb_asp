using StocksApi.BusinessLogicalLayer.Services.Base;

namespace StocksApi.BusinessLogicalLayer.Services;

public class ExternalQueryService : IExternalQueryService
{

    private readonly ExternalLinks _links;
    private readonly HttpClient _httpClient;

    public ExternalQueryService(HttpClient httpClient, ExternalLinks links)
    {
        _httpClient = httpClient;
        _links = links;
    }

    public async Task<bool> IsProductExist(Guid Id)
    {
        var data = await _httpClient.GetAsync(_links.ProductExistUrl + Id);
        return await data.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> IsStoreExist(Guid Id)
    {
        var data = await _httpClient.GetAsync(_links.StoreExistUrl + Id);
        return await data.Content.ReadFromJsonAsync<bool>();
    }
}
