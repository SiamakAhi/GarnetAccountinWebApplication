using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_InventoryMapp : IEntityTypeConfiguration<Wh_Inventory>
    {
        public void Configure(EntityTypeBuilder<Wh_Inventory> builder)
        {
            builder.HasKey(k => k.InventoryId);

            builder.HasOne(n => n.BaseUnit)
                .WithMany(n => n.InventoriesBaseUnit)
                .HasForeignKey(n => n.BaseUnitId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.PakageUnit)
                .WithMany(n => n.InventoriesPackageUnit)
                .HasForeignKey(n => n.PackageUnitId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
