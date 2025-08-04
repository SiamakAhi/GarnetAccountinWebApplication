using GarnetAccounting.Areas.AvaRasta.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.AvaRasta.Models.Mapping
{
    public class ModuleMapp : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Module { Id = Guid.NewGuid(), Name = "حسابداری", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "خزانه داری", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "بازرگانی", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "مدیریت انبار", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "حقوق و دستمزد", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "سامانه مودیان", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "اموال", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "آرشیو اسناد", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "مدیریت ارتباط با مشتری", Description = "" },
                new Module { Id = Guid.NewGuid(), Name = "ماژول فروش خدمات بار", Description = "" }
                );
        }
    }
}
