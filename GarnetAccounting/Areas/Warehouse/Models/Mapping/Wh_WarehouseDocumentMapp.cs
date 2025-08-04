using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_WarehouseDocumentMapp : IEntityTypeConfiguration<Wh_WarehouseDocument>
    {
        public void Configure(EntityTypeBuilder<Wh_WarehouseDocument> builder)
        {
            builder.HasKey(k => k.WarehouseDocumentId);

            builder.HasOne(n => n.SourceWarehouse)
                .WithMany(n => n.WarehouseDocuments)
                .HasForeignKey(n => n.SourceWarehouseId);

            builder.HasOne(n => n.DestinationWarehouse)
                .WithMany()
                .HasForeignKey(n => n.DestinationWarehouseId);
        }
    }
}
