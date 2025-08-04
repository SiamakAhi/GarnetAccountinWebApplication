using GarnetAccounting.Areas.AutoServiceAgency.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.AutoServiceAgency.Models.EntitiesConfig
{
    public class asa_Contractor_Config : IEntityTypeConfiguration<asa_Contractor>
    {
        public void Configure(EntityTypeBuilder<asa_Contractor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ContractorTafsil)
                .WithMany(x => x.asaContractors)
                .HasForeignKey(x => x.TafsilId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
