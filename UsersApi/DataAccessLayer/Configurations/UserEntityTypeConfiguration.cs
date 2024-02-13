using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersApi.DataAccessLayer.Models;

namespace UsersApi.DataAccessLayer.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("user_id").IsRequired();

        builder.Property(x => x.Email).HasColumnName("email").IsRequired();
        builder.Property(x => x.Password).HasColumnName("password").IsRequired();
        builder.Property(x => x.Salt).HasColumnName("salt").IsRequired();
        builder.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();

        builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
    }
}
