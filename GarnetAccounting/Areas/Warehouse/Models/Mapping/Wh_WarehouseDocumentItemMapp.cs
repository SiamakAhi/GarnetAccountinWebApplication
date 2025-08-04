using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_WarehouseDocumentItemMapp : IEntityTypeConfiguration<Wh_WarehouseDocumentItem>
    {
        public void Configure(EntityTypeBuilder<Wh_WarehouseDocumentItem> builder)
        {
            builder.HasKey(k => k.DocumentLineId);

            builder.HasOne(n => n.WarehouseDocument)
                .WithMany(n => n.DocumentItems)
                .HasForeignKey(n => n.WarehouseDocumentId);

            builder.HasOne(n => n.Product)
                .WithMany(n => n.WarehouseDocumentItems)
                .HasForeignKey(n => n.ProductId);


        }
    }
}
