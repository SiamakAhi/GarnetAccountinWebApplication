using GarnetAccounting.Areas.Treasury.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Treasury.Models.Mapping
{
    public class TreCarrency_Map : IEntityTypeConfiguration<TreCarrency>
    {
        public void Configure(EntityTypeBuilder<TreCarrency> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasData(
                new TreCarrency { Id = 1, SellerId = 120, FullName = "ریال ایران", ShortName = "IRR", ExchangeRateToRial = 1 },
                new TreCarrency { Id = 2, SellerId = 120, FullName = "دلار آمریکا", ShortName = "USD", ExchangeRateToRial = 770000 },
                new TreCarrency { Id = 3, SellerId = 120, FullName = "یورو", ShortName = "EUR", ExchangeRateToRial = 810000 },
                new TreCarrency { Id = 4, SellerId = 120, FullName = "پوند انگلیس", ShortName = "GBP", ExchangeRateToRial = 920000 },
                new TreCarrency { Id = 5, SellerId = 120, FullName = "یوآن چین", ShortName = "CNY", ExchangeRateToRial = 6500 },
                new TreCarrency { Id = 6, SellerId = 120, FullName = "درهم امارات", ShortName = "AED", ExchangeRateToRial = 11400 },
                new TreCarrency { Id = 7, SellerId = 120, FullName = "لیره ترکیه", ShortName = "TRY", ExchangeRateToRial = 6000 },
                new TreCarrency { Id = 8, SellerId = 120, FullName = "دینار عراق", ShortName = "IQD", ExchangeRateToRial = 35 },
                new TreCarrency { Id = 9, SellerId = 120, FullName = "ریال قطر", ShortName = "QAR", ExchangeRateToRial = 11500 },
                new TreCarrency { Id = 10, SellerId = 120, FullName = "ریال سعودی", ShortName = "SAR", ExchangeRateToRial = 11200 },
                new TreCarrency { Id = 11, SellerId = 120, FullName = "دینار کویت", ShortName = "KWD", ExchangeRateToRial = 140000 },
                new TreCarrency { Id = 12, SellerId = 120, FullName = "ین ژاپن", ShortName = "JPY", ExchangeRateToRial = 380 }
                // ارزهای دیگر می‌توانند به این لیست اضافه شوند
            );
        }
    }
}
