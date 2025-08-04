using GarnetAccounting.Models.Commercial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Models.Mapping
{
    public class PartyRepresentative_Map : IEntityTypeConfiguration<PartyRepresentative>
    {
        public void Configure(EntityTypeBuilder<PartyRepresentative> builder)
        {
            builder
            .HasKey(pr => new { pr.PartyId, pr.RepresentativeId });

            builder
                .HasOne(pr => pr.Party)
                .WithMany(p => p.PartyRepresentatives)
                .HasForeignKey(pr => pr.PartyId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
