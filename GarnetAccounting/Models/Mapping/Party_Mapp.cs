using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Models.Mapping
{
    public class Party_Mapp : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.PartyCustomer)
                .WithMany(p => p.CustomerParties)
                .HasForeignKey(f => f.CustomerId);

            builder.HasOne(p => p.PayerType)
               .WithMany(p => p.Parties)
               .HasForeignKey(f => f.TaxPayerType);
        }
    }
}
