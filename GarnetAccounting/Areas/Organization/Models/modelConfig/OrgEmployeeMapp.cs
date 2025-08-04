using GarnetAccounting.Areas.Organization.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Organization.Models.modelConfig
{
    public class OrgEmployeeMapp : IEntityTypeConfiguration<OrgEmployee>
    {
        public void Configure(EntityTypeBuilder<OrgEmployee> builder)
        {
            builder.HasKey(n => n.Id); // کلید اصلی

            builder.Property(n => n.PersonId)
                .IsRequired(); // شناسه شخص اجباری است

            builder.Property(n => n.RelationshipType)
                .IsRequired(); // نوع رابطه اجباری است

            builder.Property(n => n.StartDate)
                .IsRequired(); // تاریخ شروع فعالیت اجباری است

            builder.HasOne(n => n.Position)
                .WithMany() // ارتباط با سمت
                .HasForeignKey(n => n.PositionId)
                .OnDelete(DeleteBehavior.Restrict); // تنظیم رفتار حذف

            builder.HasOne(n => n.Supervisor) // ارتباط با سرپرست
                .WithMany()
                .HasForeignKey(n => n.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict); // تنظیم رفتار حذف
        }
    }
}
