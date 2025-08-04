using GarnetAccounting.Areas.Commercial.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Commercial.Models.Mapping
{
    public class Com_InvoiceMapp : IEntityTypeConfiguration<com_Invoice>
    {
        public void Configure(EntityTypeBuilder<com_Invoice> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(n => n.InvoiceParty)
                .WithMany(n => n.Invoices)
                .HasForeignKey(f => f.PartyId);

            builder.HasOne(n => n.InvoiceProject)
               .WithMany(n => n.ProjectInvoices)
               .HasForeignKey(f => f.projectId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
