using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UsersApi.DataAccessLayer.Contexts;

public class UserContextDesignTimeFactory : IDesignTimeDbContextFactory<UserContext>
{
    public UserContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=user_db;Username=postgres;Password=password");

        return new UserContext(optionsBuilder.Options);
    }
}