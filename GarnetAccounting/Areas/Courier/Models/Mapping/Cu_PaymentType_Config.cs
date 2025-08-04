using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_PaymentType_Config : IEntityTypeConfiguration<Cu_PaymentType>
    {
        public void Configure(EntityTypeBuilder<Cu_PaymentType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                  new Cu_PaymentType { Id = 1, Name = "نقد" },
                  new Cu_PaymentType { Id = 2, Name = "واریز به حساب" },
                  new Cu_PaymentType { Id = 3, Name = "کارتخوان" },
                  new Cu_PaymentType { Id = 4, Name = "درگاه پرداخت" },
                  new Cu_PaymentType { Id = 5, Name = "چک" }
                );

        }
    }
}
