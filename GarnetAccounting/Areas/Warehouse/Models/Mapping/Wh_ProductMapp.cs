using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_ProductMapp : IEntityTypeConfiguration<Wh_Product>
    {
        public void Configure(EntityTypeBuilder<Wh_Product> builder)
        {
            builder.HasKey(k => k.ProductId);

            builder.HasOne(n => n.ProductCategory)
                .WithMany(n => n.Products)
                .HasForeignKey(n => n.CategoryId);

            builder.HasOne(n => n.BaseUnit)
                .WithMany(n => n.Products)
                .HasForeignKey(n => n.BaseUnitId);

            builder.HasOne(n => n.PakageUnit)
              .WithMany(n => n.PakageProducts)
              .HasForeignKey(n => n.PakageCountId);

        }
    }
}
