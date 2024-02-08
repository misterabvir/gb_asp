using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductPersistence.Contexts;


namespace ProductPersistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ProductContext>(
            options => options
                .UseNpgsql(configuration
                .GetConnectionString("StoreConnection")));
        
        return services;
    }

}
