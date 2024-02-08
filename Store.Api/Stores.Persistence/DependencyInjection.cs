using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorePersistence.Contexts;


namespace StorePersistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(
            options => options
                .UseNpgsql(configuration
                .GetConnectionString("StoreConnection")));
        
        return services;
    }

}
