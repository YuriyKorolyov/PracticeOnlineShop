using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Configurations
{
    /// <summary>
    /// Конфигурация для сущности ProductCategory.
    /// </summary>
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        /// <summary>
        /// Настраивает сущность ProductCategory.
        /// </summary>
        /// <param name="builder">Строитель для сущности ProductCategory.</param>
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories) 
                .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories) 
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
