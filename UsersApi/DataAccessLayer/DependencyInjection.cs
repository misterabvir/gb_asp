using Microsoft.EntityFrameworkCore;
using UsersApi.DataAccessLayer.Contexts;

namespace UsersApi.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UserContext>(options=>
            options.UseNpgsql(
                configuration.GetConnectionString("DataBaseConnection")
                ?? throw new InvalidOperationException("Connection string not found")
        ));


        return services;     
    }
}
