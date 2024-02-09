using System.Net;

namespace GraphQlApi.Services;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> Get<T>(string url)
    {
        var result = await _httpClient.GetFromJsonAsync<T>(url);
        return result;
    }

    public async Task<HttpStatusCode> Post<T>(string url, T request)
    {
        var result = await _httpClient.PostAsJsonAsync(url, request);
        return result.StatusCode;
    }

    public async Task<HttpStatusCode> Put<T>(string url, T request)
    {
        var result = await _httpClient.PutAsJsonAsync(url, request);
        return result.StatusCode;
    }

    public async Task<HttpStatusCode> Delete<T>(string url, T request)
    {
        var result = await _httpClient.SendAsync(
            new HttpRequestMessage(HttpMethod.Delete, url)
            { Content = JsonContent.Create(request) });
        return result.StatusCode;
    }
}
