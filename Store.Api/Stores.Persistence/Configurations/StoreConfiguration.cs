using StoreDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StorePersistence.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.IdentityNumber).IsUnique();
        builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(6);
    }
}
