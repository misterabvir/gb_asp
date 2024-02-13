using Microsoft.EntityFrameworkCore;
using UsersApi.DataAccessLayer.Models;

namespace UsersApi.DataAccessLayer.Contexts;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
    }
}
