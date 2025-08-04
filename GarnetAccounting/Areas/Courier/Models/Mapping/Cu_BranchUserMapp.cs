using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Courier.Models.Mapping
{
    public class Cu_BranchUserMapp : IEntityTypeConfiguration<Cu_BranchUser>
    {
        public void Configure(EntityTypeBuilder<Cu_BranchUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(b => b.Branch)
                .WithMany(b => b.BranchUsers)
                .HasForeignKey(b => b.BranchId);

            builder.HasOne(b => b.IdentityUser)
               .WithOne(b => b.BranchUser)
               .HasForeignKey<Cu_BranchUser>(b => b.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
