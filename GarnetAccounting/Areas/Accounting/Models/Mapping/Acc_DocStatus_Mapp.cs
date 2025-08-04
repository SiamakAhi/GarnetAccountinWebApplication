using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_DocStatus_Mapp : IEntityTypeConfiguration<Acc_DocStatus>
    {
        public void Configure(EntityTypeBuilder<Acc_DocStatus> builder)
        {

            builder.HasData(
                new Acc_DocStatus { Id = 1, Name = "یادداشت" },
                new Acc_DocStatus { Id = 2, Name = "ثبت موقت" },
                new Acc_DocStatus { Id = 3, Name = "ثبت دائم" }
                );
        }
    }
}
