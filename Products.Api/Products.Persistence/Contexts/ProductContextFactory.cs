using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductPersistence.Contexts;

public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
{
    public ProductContext CreateDbContext(string[] args) =>
        new (new DbContextOptionsBuilder<ProductContext>().UseNpgsql("Host=localhost;Port=5432;Database=market_product_db;Username=postgres;Password=password").Options);
}
