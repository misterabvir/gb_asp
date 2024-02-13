using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersApi.DataAccessLayer.Models;

namespace UsersApi.DataAccessLayer.Configurations;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("role_id").IsRequired();

        builder.Property(x => x.RoleType).HasColumnName("role_type").IsRequired();
    }
}