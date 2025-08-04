using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_HubMapp : IEntityTypeConfiguration<Cu_Hub>
    {
        public void Configure(EntityTypeBuilder<Cu_Hub> builder)
        {
            builder.HasKey(n => n.HubId);

            builder.HasOne(n => n.HubCity)
                .WithMany(n => n.CityHubs)
                .HasForeignKey(n => n.CityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
