using StoreDomain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace StorePersistence.Contexts;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Stock> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
