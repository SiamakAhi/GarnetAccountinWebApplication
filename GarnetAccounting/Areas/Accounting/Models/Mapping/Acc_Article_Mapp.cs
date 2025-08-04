using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_Article_Mapp : IEntityTypeConfiguration<Acc_Article>
    {
        public void Configure(EntityTypeBuilder<Acc_Article> builder)
        {

            builder.HasOne(n => n.Doc)
                .WithMany(n => n.DocArticles)
                .HasForeignKey(f => f.DocId);

            builder.HasOne(n => n.Moein)
             .WithMany(n => n.Articles)
             .HasForeignKey(f => f.MoeinId);

            builder.HasOne(n => n.Project)
            .WithMany(n => n.AccArticles)
            .HasForeignKey(f => f.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
