using GarnetAccounting.Areas.Projects.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Projects.Models.EntityConfigs
{
    public class Con_Project_Config : IEntityTypeConfiguration<Con_Project>
    {
        public void Configure(EntityTypeBuilder<Con_Project> builder)
        {
            builder.HasKey(k => k.Id);


            builder.HasOne(c => c.Client)
                .WithMany(x => x.ClientProjects)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
