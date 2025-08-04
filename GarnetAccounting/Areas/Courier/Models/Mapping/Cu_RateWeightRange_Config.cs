using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_RateWeightRange_Config : IEntityTypeConfiguration<Cu_RateWeightRange>
    {
        public void Configure(EntityTypeBuilder<Cu_RateWeightRange> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasData(
            new Cu_RateWeightRange { Id = 1, StartWeight = 0.001, EndWeight = 0.500, Courier_WeightFactorPercent = 0, IATA_WeightFactorPercent = 0 },
            new Cu_RateWeightRange { Id = 2, StartWeight = 0.501, EndWeight = 1.000, Courier_WeightFactorPercent = 0, IATA_WeightFactorPercent = 0 },
            new Cu_RateWeightRange { Id = 3, StartWeight = 1.001, EndWeight = 1.500, Courier_WeightFactorPercent = 0, IATA_WeightFactorPercent = 0 },
            new Cu_RateWeightRange { Id = 4, StartWeight = 1.501, EndWeight = 2.000, Courier_WeightFactorPercent = 0, IATA_WeightFactorPercent = 0 }

            );
        }
    }
}
