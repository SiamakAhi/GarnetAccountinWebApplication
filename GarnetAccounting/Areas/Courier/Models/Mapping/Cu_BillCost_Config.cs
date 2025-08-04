using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_BillCost_Config : IEntityTypeConfiguration<Cu_BillCost>
    {
        public void Configure(EntityTypeBuilder<Cu_BillCost> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.BillOfLading)
                .WithMany(n => n.BillCosts)
                .HasForeignKey(n => n.BillOfLadingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.Consignment)
           .WithMany(n => n.BillCosts)
           .HasForeignKey(n => n.ConsignmentId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.CostType)
           .WithMany(n => n.BillCosts)
           .HasForeignKey(n => n.CostTypeId)
           .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
