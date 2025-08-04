using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_RateImpactType_Config : IEntityTypeConfiguration<Cu_RateImpactType>
    {
        public void Configure(EntityTypeBuilder<Cu_RateImpactType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                  new Cu_RateImpactType { Id = 1, RateImpactTypeCode = 1, Name = "ثابت" },
                  new Cu_RateImpactType { Id = 2, RateImpactTypeCode = 2, Name = "درصداز کل بارنامه" },
                  new Cu_RateImpactType { Id = 3, RateImpactTypeCode = 3, Name = "درصد از مبلغ حمل بار" },
                  new Cu_RateImpactType { Id = 4, RateImpactTypeCode = 4, Name = "محاسبه توسط کاربر" },
                  new Cu_RateImpactType { Id = 5, RateImpactTypeCode = 4, Name = "محاسبه سیستمی" }
                );
        }
    }
}
