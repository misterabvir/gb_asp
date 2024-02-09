using Microsoft.EntityFrameworkCore;
using ProductsApi.DataAccessLayer.Models;

namespace ProductsApi.DataAccessLayer.Contexts;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    { }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);

            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
        });

        modelBuilder.Entity<Category>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired();
        });
    }
}
