using GarnetAccounting.Areas.AutoServiceAgency.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.AutoServiceAgency.Models.EntitiesConfig
{
    public class asa_Reception_Config : IEntityTypeConfiguration<asa_Reception>
    {
        public void Configure(EntityTypeBuilder<asa_Reception> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.Receptions)
                .HasForeignKey(x => x.ContractorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.MechanicContractor)
              .WithMany(x => x.MechanicReceptions)
              .HasForeignKey(x => x.lamariMechanicContractorId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ElectricContractor)
             .WithMany(x => x.ElectrincReceptions)
             .HasForeignKey(x => x.lamariElectricContractorId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Service)
               .WithMany(x => x.Receptions)
               .HasForeignKey(x => x.ServiceId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
