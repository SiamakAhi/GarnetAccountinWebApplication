using GarnetAccounting.Areas.Treasury.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Treasury.Models.Mapping
{
    public class TreChequeOperationMap : IEntityTypeConfiguration<TreChequeOperation>
    {
        public void Configure(EntityTypeBuilder<TreChequeOperation> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OperationType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Description)
                .HasMaxLength(500);

            // Seeding data with HasData
            builder.HasData(
                new TreChequeOperation { Id = 1, OperationType = "چک دریافتی", Description = "دریافت چک" },
                new TreChequeOperation { Id = 2, OperationType = "چک پرداختی", Description = "پرداخت چک" },
                new TreChequeOperation { Id = 3, OperationType = "در جریان وصول", Description = "چک در جریان وصول" },
                new TreChequeOperation { Id = 4, OperationType = "پاس چک", Description = "پاس چک" },
                new TreChequeOperation { Id = 5, OperationType = "برگشت چک", Description = "برگشت چک" },
                new TreChequeOperation { Id = 6, OperationType = "عودت چک برگشتی", Description = "عودت چک برگشتی" },
                new TreChequeOperation { Id = 7, OperationType = "واخواست", Description = "واخواست چک" },
                new TreChequeOperation { Id = 8, OperationType = "دریافت چک پاس نشده یا برگشتی", Description = "دریافت چک پاس نشده یا برگشتی" },
                new TreChequeOperation { Id = 9, OperationType = "سفته دریافتی", Description = "دریافت سفته" },
                new TreChequeOperation { Id = 10, OperationType = "سفته پرداختی", Description = "پرداخت سفته" }
            );
        }
    }
}
