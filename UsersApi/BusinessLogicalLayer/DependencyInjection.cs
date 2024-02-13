using UsersApi.BusinessLogicalLayer.Services;
using UsersApi.BusinessLogicalLayer.Services.Base;

namespace UsersApi.BusinessLogicalLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicalLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options => {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            options.InstanceName = "UsersApi";
        });

        services.AddScoped<IEncryptService, EncryptService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
