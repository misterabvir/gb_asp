using ProductsApi.BusinessLogicalLayer.Services.Base;

namespace ProductsApi.BusinessLogicalLayer.Services;

public class ExternalQueryService : IExternalQueryService
{

    private readonly ExternalLinks _links;
    private readonly HttpClient _httpClient;

    public ExternalQueryService(HttpClient httpClient, ExternalLinks links)
    {
        _httpClient = httpClient;
        _links = links;
    }


    public async Task<bool> IsStockExist(Guid Id)
    {
        var data = await _httpClient.GetAsync(_links.StockExistUrl + Id);
        return await data.Content.ReadFromJsonAsync<bool>();
    }


}
