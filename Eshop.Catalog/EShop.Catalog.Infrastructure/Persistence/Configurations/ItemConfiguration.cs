using EShop.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Infrastructure.Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.HasKey(k => k.Id);

            builder.HasOne(x => x.Category).WithOne().HasForeignKey<Item>(x=> x.CategoryId)
                .IsRequired();
        }
    }
}
