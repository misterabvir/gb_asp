using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StoresApi.DataAccessLayer.Contexts;

public class StoresDesignTimeFactory : IDesignTimeDbContextFactory<StoresContext>
{
    public StoresContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoresContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=store_db;Username=postgres;Password=password");
        return new StoresContext(optionsBuilder.Options);
    }
}