using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_RateZone_Config : IEntityTypeConfiguration<Cu_RateZone>
    {
        public void Configure(EntityTypeBuilder<Cu_RateZone> builder)
        {
            builder.HasKey(n => n.ZoneId);

            builder.HasData(
                      new Cu_RateZone { ZoneId = 1, SellerId = 3, Name = "زون 1", IsSatellite = false },
                      new Cu_RateZone { ZoneId = 2, SellerId = 3, Name = "زون 2", IsSatellite = false },
                      new Cu_RateZone { ZoneId = 3, SellerId = 3, Name = "زون 3", IsSatellite = false }
                );
        }
    }
}
