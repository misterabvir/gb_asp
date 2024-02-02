using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Repositories.Abstractions;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(
            options => options
                .UseSqlServer(configuration
                .GetConnectionString("StoreConnection")));

        services.AddRepositories();
        
        return services;
    }

    public static IServiceCollection AddRepositories(
    this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IStockRepository, StockRepository>();

        return services;
    }
}
