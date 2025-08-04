using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_Packaging_Config : IEntityTypeConfiguration<Cu_Packaging>
    {
        public void Configure(EntityTypeBuilder<Cu_Packaging> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                 new Cu_Packaging { Id = 1, SellerId = 3, PackageCode = "80", Name = "پاکت", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 2, SellerId = 3, PackageCode = "81", Name = "کارتن", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 3, SellerId = 3, PackageCode = "82", Name = "یونولیت", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 4, SellerId = 3, PackageCode = "83", Name = "کلد باکس", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 5, SellerId = 3, PackageCode = "84", Name = "جعبه چوبی", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 6, SellerId = 3, PackageCode = "85", Name = "جعبه فلزی", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 7, SellerId = 3, PackageCode = "86", Name = "پالت", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 8, SellerId = 3, PackageCode = "87", Name = "چمدان", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 9, SellerId = 3, PackageCode = "88", Name = "نایلون پیچ", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 10, SellerId = 3, PackageCode = "89", Name = "پاکت و کارتن", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 11, SellerId = 3, PackageCode = "90", Name = "پاکت و نایلون", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 12, SellerId = 3, PackageCode = "91", Name = "پاکت و یونولیت", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 13, SellerId = 3, PackageCode = "92", Name = "پاکت و کارتن و نایلون", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 14, SellerId = 3, PackageCode = "93", Name = "پاکت و نایلون و کارتن و یونولیت", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 15, SellerId = 3, PackageCode = "94", Name = "کارتن و جعبه چوبی", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 16, SellerId = 3, PackageCode = "95", Name = "کارتن و جعبه چوبی و نایلون", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 17, SellerId = 3, PackageCode = "96", Name = "جعبه چوبی و نایلون", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 18, SellerId = 3, PackageCode = "97", Name = "باکس پزشکی", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 19, SellerId = 3, PackageCode = "98", Name = "پالت و کارتن", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 20, SellerId = 3, PackageCode = "99", Name = "کارتن و نایلون پیچ", Price = 0, ForExport = false },
                 new Cu_Packaging { Id = 21, SellerId = 3, PackageCode = "100", Name = "box", Price = 0, ForExport = true },
                 new Cu_Packaging { Id = 22, SellerId = 3, PackageCode = "101", Name = "کیسه", Price = 0, ForExport = false }
             );

        }
    }
}
