using GarnetAccounting.Areas.Commercial.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Commercial.Models.Mapping
{
    public class Com_InvoiceItemMapp : IEntityTypeConfiguration<com_InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<com_InvoiceItem> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(n => n.Invoice)
                .WithMany(n => n.InvoiceItems)
                .HasForeignKey(f => f.InvoiceId);

            builder.HasOne(n => n.Product)
                .WithMany(n => n.InvoiceItems)
                .HasForeignKey(f => f.ProductId);
        }
    }
}
