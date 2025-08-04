using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_Route_Config : IEntityTypeConfiguration<Cu_Route>
    {
        public void Configure(EntityTypeBuilder<Cu_Route> builder)
        {
            builder.HasKey(k => k.RouteId);

            builder.HasOne(a => a.OriginCity)
                .WithMany(a => a.OriginCities)
                .HasForeignKey(a => a.OriginCityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.DestinationCity)
              .WithMany(a => a.DestinationCities)
              .HasForeignKey(a => a.DestinationCityId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Zone)
             .WithMany(a => a.Cu_Routes)
             .HasForeignKey(a => a.ZoneId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
