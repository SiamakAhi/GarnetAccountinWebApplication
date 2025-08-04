using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_RateBaseKValue_Config : IEntityTypeConfiguration<Cu_RateBaseKValue>
    {

        public void Configure(EntityTypeBuilder<Cu_RateBaseKValue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Cu_RateBaseKValue { Id = 1, KValue = 10000, SellerId = 3 },
                new Cu_RateBaseKValue { Id = 2, KValue = 15000, SellerId = 120 }
                );
        }
    }
}
