using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GarnetAccounting.Models.Commercial;

namespace GarnetAccounting.Models.Mapping
{
    public class PartyType_Mapp : IEntityTypeConfiguration<PartyType>
    {
        public void Configure(EntityTypeBuilder<PartyType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new PartyType { Id = 1, Code = "1", Name = "حقیقی" },
                new PartyType { Id = 2, Code = "2", Name = "حقوقی" },
                new PartyType { Id = 3, Code = "3", Name = "مشارکت مدنی" },
                new PartyType { Id = 4, Code = "4", Name = "اتباع غیرایرانی" },
                new PartyType { Id = 5, Code = "5", Name = "مصرف کننده نهایی" }

                );
        }
    }
}
