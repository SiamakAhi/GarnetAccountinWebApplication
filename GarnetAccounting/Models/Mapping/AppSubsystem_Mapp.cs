using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Models.Mapping
{
    public class AppSubsystem_Mapp : IEntityTypeConfiguration<AppSubsystem>
    {
        public void Configure(EntityTypeBuilder<AppSubsystem> builder)
        {
            builder.HasData(
                new AppSubsystem { Id = 1, Name_En = "Accounting", Name_fa = "حسابداری", Description = "" },
                new AppSubsystem { Id = 2, Name_En = "Buy", Name_fa = "خرید", Description = "" },
                new AppSubsystem { Id = 3, Name_En = "Sale", Name_fa = "فروش", Description = "" },
                new AppSubsystem { Id = 4, Name_En = "Warehouse", Name_fa = "انبار", Description = "" },
                new AppSubsystem { Id = 5, Name_En = "Khazane", Name_fa = "خزانه داری", Description = "" },
                new AppSubsystem { Id = 6, Name_En = "Asset", Name_fa = "اموال", Description = "" },
                new AppSubsystem { Id = 7, Name_En = "Contract", Name_fa = "قراردادها", Description = "" },
                new AppSubsystem { Id = 8, Name_En = "Hoghoogh", Name_fa = "حقوق و دستمزد", Description = "" }
                );
        }
    }
}
