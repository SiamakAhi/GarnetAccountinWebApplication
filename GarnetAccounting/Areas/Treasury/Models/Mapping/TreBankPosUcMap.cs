using GarnetAccounting.Areas.Treasury.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Treasury.Models.Mapping
{
    public class TreBankPosUcMap : IEntityTypeConfiguration<TreBankPosUc>
    {
        public void Configure(EntityTypeBuilder<TreBankPosUc> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // تعریف مقادیر پیش‌فرض
            builder.HasData(
                new TreBankPosUc { Id = 1, Name = "آسان پرداخت" },
                new TreBankPosUc { Id = 2, Name = "بانک ملت" },
                new TreBankPosUc { Id = 3, Name = "بانک ملت - سرويس" },
                new TreBankPosUc { Id = 4, Name = "بانک ملي" },
                new TreBankPosUc { Id = 5, Name = "تجارت الکترونيک پارسيان" },
                new TreBankPosUc { Id = 6, Name = "سامان کيش" },
                new TreBankPosUc { Id = 7, Name = "ندارد" }
            );
        }
    }
}
