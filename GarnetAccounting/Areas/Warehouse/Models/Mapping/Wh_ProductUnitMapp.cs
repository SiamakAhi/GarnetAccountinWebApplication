using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_ProductUnitMapp : IEntityTypeConfiguration<Wh_ProductUnit>
    {
        public void Configure(EntityTypeBuilder<Wh_ProductUnit> builder)
        {
            builder.HasKey(k => k.ProductUnitId);

            builder.HasOne(n => n.Product)
                .WithMany(n => n.ProductUnits)
                .HasForeignKey(n => n.ProductId);

            builder.HasOne(n => n.UnitOfMeasure)
                .WithMany(n => n.ProductUnits)
                .HasForeignKey(n => n.UnitOfMeasureId);
        }
    }
}
