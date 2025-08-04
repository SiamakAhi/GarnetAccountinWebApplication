using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_InventoryTransactionMapp : IEntityTypeConfiguration<Wh_InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<Wh_InventoryTransaction> builder)
        {
            builder.HasKey(k => k.TransactionId);

            builder.HasOne(n => n.Product)
                .WithMany()
                .HasForeignKey(n => n.ProductId);

            builder.HasOne(n => n.WarehouseDocument)
                .WithMany()
                .HasForeignKey(n => n.WarehouseDocumentId);

            builder.HasOne(n => n.UnitOfMeasure)
                .WithMany()
                .HasForeignKey(n => n.UnitOfMeasureId);

            builder.HasOne(n => n.SourceWarehouse)
                .WithMany()
                .HasForeignKey(n => n.SourceWarehouseId);

            builder.HasOne(n => n.DestinationWarehouse)
                .WithMany()
                .HasForeignKey(n => n.DestinationWarehouseId);
        }
    }
}
