using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace StoreApplication;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CacheService(IDistributedCache cache, DistributedCacheEntryOptions options)
    {
        _cache = cache;
        _options = options;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
    {
        var data = await _cache.GetAsync(key, cancellationToken);
        if (data is null)
        {
            return default;
        }
        var json = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(key, cancellationToken);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(value);
        var data = Encoding.UTF8.GetBytes(json);
        await _cache.SetAsync( key, data, _options, cancellationToken);
        ;
    }
}
