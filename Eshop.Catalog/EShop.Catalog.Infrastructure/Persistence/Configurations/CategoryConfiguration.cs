using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EShop.Catalog.Domain.Entities;

namespace EShop.Catalog.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.HasKey(k => k.Id);

            builder.HasOne(x => x.ParentCategory).WithMany(x => x.ChildCategory).HasForeignKey(x => x.ParentCategoryId)
                .IsRequired(false);
        }
    }
}
