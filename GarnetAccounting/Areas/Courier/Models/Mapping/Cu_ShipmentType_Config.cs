using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_ShipmentType_Config : IEntityTypeConfiguration<Cu_ShipmentType>
    {

        public void Configure(EntityTypeBuilder<Cu_ShipmentType> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasData(
                 new Cu_ShipmentType { Id = 1, Code = 1, Name = "زمینی" },
                 new Cu_ShipmentType { Id = 2, Code = 2, Name = "هوایی" },
                 new Cu_ShipmentType { Id = 3, Code = 3, Name = "دریایی" }
                 );

        }
    }
}
