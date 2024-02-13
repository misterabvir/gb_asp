using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using UsersApi.BusinessLogicalLayer.Services.Base;

namespace UsersApi.BusinessLogicalLayer.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;
    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) };
    }

    public async Task<T?> Get<T>(string key)
    {
        var data = await _cache.GetAsync(key);
        if (data == null)
        {
            return default;
        }
        var json = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task Set<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var data = Encoding.UTF8.GetBytes(json);
        await _cache.SetAsync(key, data, _options);
    }

    public async Task Remove(string key)
    {
        await _cache.RemoveAsync(key);
    }
}
