using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Models.Mapping
{
    public class AppThemeMapp : IEntityTypeConfiguration<AppTheme>
    {
        public void Configure(EntityTypeBuilder<AppTheme> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasData(
                new AppTheme { Id = 1, StyleFileName = "bootstrap.morph.rtl.min.css", CssClass = "morph", IsDark = false },
                new AppTheme { Id = 2, StyleFileName = "bootstrap.solar.rtl.min.css", CssClass = "solar", IsDark = true }
                );
        }
    }
}
