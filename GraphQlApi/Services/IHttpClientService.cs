using System.Net;

namespace GraphQlApi.Services;

public interface IHttpClientService
{
    Task<T?> Get<T>(string url);
    Task<HttpStatusCode> Post<T>(string url, T request);
    Task<HttpStatusCode> Put<T>(string url, T request);
    Task<HttpStatusCode> Delete<T>(string url, T request);
}
