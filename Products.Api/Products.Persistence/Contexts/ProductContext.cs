using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProductPersistence.Contexts;

public class ProductContext(DbContextOptions options) : DbContext(options)  
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
