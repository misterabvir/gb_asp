using System.Net;

namespace ExternalLinks.Base;

public interface IHttpClientService
{
    Task<T?> Get<T>(string url);
    Task<string> Post<T>(string url, T request);
    Task<string> Put<T>(string url, T request);
    Task<string> Delete<T>(string url, T request);
}
