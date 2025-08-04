using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_ConsignmentNature_Config : IEntityTypeConfiguration<Cu_ConsignmentNature>
    {
        public void Configure(EntityTypeBuilder<Cu_ConsignmentNature> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.RateImpactType)
                .WithMany(n => n.ConsignmentNatures)
                .HasForeignKey(n => n.RateImpactTypeId)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
