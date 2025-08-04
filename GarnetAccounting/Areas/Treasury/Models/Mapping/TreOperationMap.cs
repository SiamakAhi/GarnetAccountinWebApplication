using GarnetAccounting.Areas.Treasury.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Treasury.Models.Mapping
{
    public class TreOperationMap : IEntityTypeConfiguration<TreOperation>
    {
        public void Configure(EntityTypeBuilder<TreOperation> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OperationType)
                .IsRequired();

            builder.Property(o => o.OperationName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.IsPOSTransaction)
                .HasDefaultValue(false); // Default value: Not a POS transaction

            // Seeding data with HasData
            builder.HasData(
                // Receipt operations
                new TreOperation { Id = 1, OperationType = 1, OperationName = "دریافت وجه نقد", IsPOSTransaction = false },
                new TreOperation { Id = 2, OperationType = 2, OperationName = "واریز به حساب (POS)", IsPOSTransaction = true },
                new TreOperation { Id = 3, OperationType = 3, OperationName = "حواله بانکی", IsPOSTransaction = false },
                new TreOperation { Id = 4, OperationType = 4, OperationName = "دریافت چک", IsPOSTransaction = false },
                new TreOperation { Id = 5, OperationType = 5, OperationName = "تهاتر", IsPOSTransaction = false },

                // Payment operations
                new TreOperation { Id = 6, OperationType = 6, OperationName = "پرداخت وجه نقد", IsPOSTransaction = false },
                new TreOperation { Id = 7, OperationType = 7, OperationName = "واریز به حساب", IsPOSTransaction = true },
                new TreOperation { Id = 8, OperationType = 8, OperationName = "حواله", IsPOSTransaction = false },
                new TreOperation { Id = 9, OperationType = 9, OperationName = "پرداخت چک", IsPOSTransaction = false }
            );
        }
    }
}
