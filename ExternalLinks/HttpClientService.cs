using System.Net;
using System.Net.Http.Json;
using ExternalLinks.Base;

namespace ExternalLinks;

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

    public async Task<string> Post<T>(string url, T request)
    {
        var result = await _httpClient.PostAsJsonAsync(url, request);
        return await Response(result);
    }

    public async Task<string> Put<T>(string url, T request)
    {
        var result = await _httpClient.PutAsJsonAsync(url, request);
        return await Response(result);
    }

    public async Task<string> Delete<T>(string url, T request)
    {
        var result = await _httpClient.SendAsync(
            new HttpRequestMessage(HttpMethod.Delete, url)
            { Content = JsonContent.Create(request) });
        return await Response(result);
    }

    private static async Task<string> Response(HttpResponseMessage result)
    {
        var body = await result.Content.ReadAsStringAsync();
        return $"Status: {(int)result.StatusCode} ({result.ReasonPhrase}); Body: {body}";
    }
}
