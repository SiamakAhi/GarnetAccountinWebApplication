using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_RepresentativeMap : IEntityTypeConfiguration<Cu_Representative>
    {
        public void Configure(EntityTypeBuilder<Cu_Representative> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.Person)
                .WithOne(n => n.Representative)
                .HasForeignKey<Cu_Representative>(n => n.PartyId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
