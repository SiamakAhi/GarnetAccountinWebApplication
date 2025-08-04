using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_Document_Mapp : IEntityTypeConfiguration<Acc_Document>
    {
        public void Configure(EntityTypeBuilder<Acc_Document> builder)
        {


            builder.HasOne(n => n.DocPeriod)
                .WithMany(n => n.Documents)
                .HasForeignKey(f => f.PeriodId);
        }
    }
}
