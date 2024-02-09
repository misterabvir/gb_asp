using Microsoft.EntityFrameworkCore;
using StoresApi.DataAccessLayer.Models;

namespace StoresApi.DataAccessLayer.Contexts;

public class StoresContext : DbContext
{
    public StoresContext(DbContextOptions<StoresContext> options) : base(options)
    { }

    public DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired();
        });
    }
}

