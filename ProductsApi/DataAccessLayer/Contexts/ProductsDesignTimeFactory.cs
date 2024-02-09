using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductsApi.DataAccessLayer.Contexts;

public class ProductsDesignTimeFactory : IDesignTimeDbContextFactory<ProductsContext>
{
    public ProductsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=product_db;Username=postgres;Password=password");
        return new ProductsContext(optionsBuilder.Options);
    }
}