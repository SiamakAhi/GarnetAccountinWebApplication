using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_Coding_TafsilToGroup_Mapp : IEntityTypeConfiguration<Acc_Coding_TafsilToGroup>
    {
        public void Configure(EntityTypeBuilder<Acc_Coding_TafsilToGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(n => n.Group)
                .WithMany(n => n.TafsilToGroups)
                .HasForeignKey(n => n.GroupId);

            builder.HasOne(n => n.TafsilAccount)
                .WithMany(n => n.TafsilToGroups)
                .HasForeignKey(n => n.TafsilId);
        }
    }
}
