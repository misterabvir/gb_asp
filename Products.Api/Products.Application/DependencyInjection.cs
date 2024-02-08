using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsApplication.Helpers;
using System.Reflection;

namespace ProductApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient<HttpClient>();

        services.AddTransient<IExternalApiLinks>(provider => configuration.GetSection("ExternalApiLinks").Get<ExternalApiLinks>() ?? throw new Exception("ExternalApiLinks not found"));

        services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("StoreCacheConnection"));
        services.AddTransient(provider =>
        {
            var minutes = configuration
                .GetSection("DistributedCacheEntryOptions")?
                .GetValue<int>("AbsoluteExpirationRelativeToNow") ?? throw new Exception("AbsoluteExpirationRelativeToNow not found");
            return new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes) };
        });
        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
}
