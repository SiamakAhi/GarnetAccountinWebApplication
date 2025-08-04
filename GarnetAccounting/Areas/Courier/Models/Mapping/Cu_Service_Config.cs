using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_Service_Config : IEntityTypeConfiguration<Cu_Service>
    {
        public void Configure(EntityTypeBuilder<Cu_Service> builder)
        {
            builder.HasKey(a => a.Id);

        }
    }
}
