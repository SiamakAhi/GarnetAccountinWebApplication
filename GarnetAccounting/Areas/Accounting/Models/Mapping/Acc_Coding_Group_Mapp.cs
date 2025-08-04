using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_Coding_Group_Mapp : IEntityTypeConfiguration<Acc_Coding_Group>
    {
        public void Configure(EntityTypeBuilder<Acc_Coding_Group> builder)
        {


            //-- TypeId :
            //----1 = ترازنامه ای
            //----2 = سود و زیانی
            //----3 = انتظامی

        }
    }
}
