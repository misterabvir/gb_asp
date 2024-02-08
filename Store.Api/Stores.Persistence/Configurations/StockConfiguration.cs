using StoreDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StorePersistence.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");
        builder.HasKey(x => new { x.ProductId, x.StoreId });

        builder.Property(x => x.Quantity).IsRequired();
        builder.HasOne<Store>().WithMany().HasForeignKey(x => x.StoreId);
    }
}