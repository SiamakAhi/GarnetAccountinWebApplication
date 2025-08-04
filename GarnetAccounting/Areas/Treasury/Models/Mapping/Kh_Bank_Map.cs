using GarnetAccounting.Areas.Treasury.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Treasury.Models.Mapping
{
    public class Kh_Bank_Map : IEntityTypeConfiguration<kh_Bank>
    {
        public void Configure(EntityTypeBuilder<kh_Bank> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasData(
                new kh_Bank { Id = 1, Name = "بانک ملی ایران" },
                new kh_Bank { Id = 2, Name = "ملت" },
                new kh_Bank { Id = 3, Name = "تجارت" },
                new kh_Bank { Id = 4, Name = "صادرات ایران" },
                new kh_Bank { Id = 5, Name = "سامان" },
                new kh_Bank { Id = 6, Name = "سپه" },
                new kh_Bank { Id = 7, Name = "پارسیان" },
                new kh_Bank { Id = 8, Name = "پاسارگاد" },
                new kh_Bank { Id = 9, Name = "مهر اقتصاد" },
                new kh_Bank { Id = 10, Name = "رفاه کارگران" },
                new kh_Bank { Id = 11, Name = "آینده" },
                new kh_Bank { Id = 12, Name = "شهر" },
                new kh_Bank { Id = 13, Name = "رسالت" },
                new kh_Bank { Id = 14, Name = "سینا" },
                new kh_Bank { Id = 15, Name = "ایران زمین" }
                );
        }
    }
}
