using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_DocType_Map : IEntityTypeConfiguration<Acc_DocType>
    {
        public void Configure(EntityTypeBuilder<Acc_DocType> builder)
        {

            builder.HasData(
                 new Acc_DocType { Id = 1, DocTypeName = "سند روزانه" },
                 new Acc_DocType { Id = 2, DocTypeName = "سند افتتاحیه" },
                 new Acc_DocType { Id = 3, DocTypeName = "سند اختتامیه" },
                 new Acc_DocType { Id = 4, DocTypeName = "سند بستن حسابهای موقت" },
                 new Acc_DocType { Id = 5, DocTypeName = "سند بستن حسابهای دائم" },
                 new Acc_DocType { Id = 6, DocTypeName = "سند طبقه بندی حسابها" }
                 );
        }
    }
}