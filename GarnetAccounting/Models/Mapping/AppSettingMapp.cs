using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Models.Mapping
{
    public class AppSettingMapp : IEntityTypeConfiguration<AppSettings>
    {
        public void Configure(EntityTypeBuilder<AppSettings> builder)
        {
            builder.HasData(new AppSettings { Id = 1, OwnerName = "سیامک آهی", AppName = "نرم افزار حسابداری گارنِت ", CompanyName = "آوا اندیش رستـا", LogoUrl = "../../img/aar.png", Version = "0.1", LoginMessage = "آوای تکنولوژی، آواز موفقیت" });
        }
    }
}
