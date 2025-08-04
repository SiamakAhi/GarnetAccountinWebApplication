using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_WarehouseLocationMapp : IEntityTypeConfiguration<Wh_WarehouseLocation>
    {
        public void Configure(EntityTypeBuilder<Wh_WarehouseLocation> builder)
        {
            builder.HasKey(k => k.LocationId);

            builder.HasOne(n => n.Warehouse)
                .WithMany(n => n.WarehouseLocations)
                .HasForeignKey(n => n.WarehouseId);

            builder.HasOne(n => n.ParentLocation)
                .WithMany(n => n.SubLocations)
                .HasForeignKey(n => n.ParentLocationId);
        }
    }
}
