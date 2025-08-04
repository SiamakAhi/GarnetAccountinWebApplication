using GarnetAccounting.Areas.Organization.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Organization.Models.modelConfig
{
    public class OrgPositionMapp : IEntityTypeConfiguration<OrgPosition>
    {
        public void Configure(EntityTypeBuilder<OrgPosition> builder)
        {
            builder.HasKey(n => n.Id); // کلید اصلی

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(150); // تنظیم حداکثر طول نام سمت

            builder.Property(n => n.Description)
                .HasMaxLength(500); // تنظیم حداکثر طول توضیحات سمت

            builder.HasOne(n => n.Department)
                .WithMany(d => d.Positions) // ارتباط سمت با بخش
                .HasForeignKey(n => n.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); // تنظیم رفتار حذف
        }
    }
}
