using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stock");
        builder.HasKey(x => new { x.ProductId, x.StoreId });

        builder.Property(x => x.Quantity).IsRequired();

        builder.HasOne(x => x.Product).WithMany(x=>x.Stocks).HasForeignKey(x => x.ProductId);
        builder.HasOne(x => x.Store).WithMany(x => x.Stocks).HasForeignKey(x => x.StoreId);
    }
}