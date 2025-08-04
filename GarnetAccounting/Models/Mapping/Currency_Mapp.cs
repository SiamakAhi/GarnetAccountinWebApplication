using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Models.Mapping
{
    public class Currency_Mapp : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Currency { Id = 1, Name = "دلار آمریکا", Code = "1" },
                new Currency { Id = 2, Name = "یورو", Code = "2" },
                new Currency { Id = 3, Name = "درهم امارات", Code = "3" },
                new Currency { Id = 4, Name = "ریال عمان", Code = "4" },
                new Currency { Id = 5, Name = "دینار کویت", Code = "5" }
                );
        }
    }
}
