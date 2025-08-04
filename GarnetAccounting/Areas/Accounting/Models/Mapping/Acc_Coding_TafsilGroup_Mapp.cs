using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_Coding_TafsilGroup_Mapp : IEntityTypeConfiguration<Acc_Coding_TafsilGroup>
    {
        public void Configure(EntityTypeBuilder<Acc_Coding_TafsilGroup> builder)
        {

            builder.HasData(
                new Acc_Coding_TafsilGroup { Id = 1, GroupName = "اشخاص و شرکت ها", Description = "مشتریان، تأمین کنندگان، کارمندان، شرکت های طرف قرارداد و ...", IsPerson = true, SellerId = null, IsEditable = false },
                new Acc_Coding_TafsilGroup { Id = 2, GroupName = "بانک ها", IsPerson = false, SellerId = null, IsEditable = false },
                new Acc_Coding_TafsilGroup { Id = 3, GroupName = "صندوق ها", IsPerson = false, SellerId = null, IsEditable = false },
                new Acc_Coding_TafsilGroup { Id = 4, GroupName = "حساب های بانکی", IsPerson = false, SellerId = null, IsEditable = false },
                new Acc_Coding_TafsilGroup { Id = 5, GroupName = "شعب", IsPerson = false, SellerId = null, IsEditable = false },
                new Acc_Coding_TafsilGroup { Id = 6, GroupName = "نمایندگی ها", IsPerson = false, SellerId = null, IsEditable = false },
                new Acc_Coding_TafsilGroup { Id = 7, GroupName = "همه", IsPerson = false, SellerId = null, IsEditable = false }
                );
        }
    }
}
