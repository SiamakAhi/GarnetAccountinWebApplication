using GarnetAccounting.Areas.Treasury.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Treasury.Models.Mapping
{
    public class TreDocumentTypeMap : IEntityTypeConfiguration<TreDocumentType>
    {
        public void Configure(EntityTypeBuilder<TreDocumentType> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DocumentTypeName)
                .IsRequired()
                .HasMaxLength(100);

            // Seeding data with HasData
            builder.HasData(
                new TreDocumentType { Id = 1, DocumentTypeName = "چک" },
                new TreDocumentType { Id = 2, DocumentTypeName = "سفته" }

            );
        }
    }
}
