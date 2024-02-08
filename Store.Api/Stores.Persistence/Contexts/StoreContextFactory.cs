using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StorePersistence.Contexts;

public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
{
    public StoreContext CreateDbContext(string[] args) =>
        new (new DbContextOptionsBuilder<StoreContext>().UseNpgsql("Host=localhost;Port=5432;Database=market_store_db;Username=postgres;Password=password").Options);
}
