using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_BillOfLadingStatus_Config : IEntityTypeConfiguration<Cu_BillOfLadingStatus>
    {
        public void Configure(EntityTypeBuilder<Cu_BillOfLadingStatus> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Cu_BillOfLadingStatus
                {
                    Id = 1,
                    Code = "1",
                    Name = "یادداشت",
                    SendNotificationToCustomer = true,
                    SendNotificationToOperations = true,
                },
                 new Cu_BillOfLadingStatus
                 {
                     Id = 2,
                     Code = "2",
                     Name = "جدید",
                     SendNotificationToCustomer = true,
                     SendNotificationToOperations = true,
                 },
                  new Cu_BillOfLadingStatus
                  {
                      Id = 3,
                      Code = "3",
                      Name = "در حال جمع آوری",
                      SendNotificationToCustomer = true,
                      SendNotificationToOperations = true,
                  },
                   new Cu_BillOfLadingStatus
                   {
                       Id = 4,
                       Code = "4",
                       Name = "ورود به هاب مبدأ",
                       SendNotificationToCustomer = true,
                       SendNotificationToOperations = true,
                   },
                    new Cu_BillOfLadingStatus
                    {
                        Id = 5,
                        Code = "5",
                        Name = "آماده رهسپاری",
                        SendNotificationToCustomer = true,
                        SendNotificationToOperations = true,
                    },
                     new Cu_BillOfLadingStatus
                     {
                         Id = 6,
                         Code = "6",
                         Name = "در حال ارسال به شهر مقصد",
                         SendNotificationToCustomer = true,
                         SendNotificationToOperations = true,
                     },
                      new Cu_BillOfLadingStatus
                      {
                          Id = 7,
                          Code = "7",
                          Name = "ورودد به هاب مقصد",
                          SendNotificationToCustomer = true,
                          SendNotificationToOperations = true,
                      },
                       new Cu_BillOfLadingStatus
                       {
                           Id = 8,
                           Code = "8",
                           Name = "در حال توزیع",
                           SendNotificationToCustomer = true,
                           SendNotificationToOperations = true,
                       },
                       new Cu_BillOfLadingStatus
                       {
                           Id = 9,
                           Code = "9",
                           Name = "تحویل شد",
                           SendNotificationToCustomer = true,
                           SendNotificationToOperations = true,
                       },
                       new Cu_BillOfLadingStatus
                       {
                           Id = 10,
                           Code = "10",
                           Name = "در حال برکشت به فرستنده",
                           SendNotificationToCustomer = true,
                           SendNotificationToOperations = true,
                       },
                       new Cu_BillOfLadingStatus
                       {
                           Id = 11,
                           Code = "11",
                           Name = "برگشت شد",
                           SendNotificationToCustomer = true,
                           SendNotificationToOperations = true,
                       }

                );
        }
    }
}
