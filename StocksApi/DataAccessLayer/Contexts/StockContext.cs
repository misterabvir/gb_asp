using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StocksApi.DataAccessLayer.Models;

namespace StocksApi.DataAccessLayer.Contexts;

public class StocksContext : DbContext
{
    public StocksContext(DbContextOptions<StocksContext> options) : base(options)
    { }
    public DbSet<Stock> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>(builder =>
        {
            builder.HasKey(p => new { p.ProductId, p.StoreId });
            builder.Property(p => p.Quantity).IsRequired();
        });
    }
}

public class StocksDesignTimeFactory : IDesignTimeDbContextFactory<StocksContext>
{
    public StocksContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StocksContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=stock_db;Username=postgres;Password=password");
        return new StocksContext(optionsBuilder.Options);
    }
}