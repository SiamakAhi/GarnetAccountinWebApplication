using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.DataTransfer.Models.ModelConfigs
{
    public class KPOldSystemSaleConfig : IEntityTypeConfiguration<KPOldSystemSaleReport>
    {
        public void Configure(EntityTypeBuilder<KPOldSystemSaleReport> builder)
        {

            builder.HasOne(n => n.DistributerBranch)
                .WithMany(n => n.Distributers)
                .HasForeignKey(n => n.DistributerId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
