using GarnetAccounting.Areas.Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Warehouse.Models.Mapping
{
    public class Wh_ProductCategoryMapp : IEntityTypeConfiguration<Wh_ProductCategory>
    {
        public void Configure(EntityTypeBuilder<Wh_ProductCategory> builder)
        {
            builder.HasKey(k => k.CategoryId);

            builder.HasOne(n => n.ParentCategory)
                .WithMany(n => n.SubCategories)
                .HasForeignKey(n => n.ParentCategoryId);
        }
    }
}
