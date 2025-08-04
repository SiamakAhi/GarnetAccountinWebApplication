using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_Coding_Kol_Mapp : IEntityTypeConfiguration<Acc_Coding_Kol>
    {
        public void Configure(EntityTypeBuilder<Acc_Coding_Kol> builder)
        {
            builder.HasOne(n => n.KolGroup)
                .WithMany(n => n.Kols)
                .HasForeignKey(f => f.GroupId);


        }
    }
}
