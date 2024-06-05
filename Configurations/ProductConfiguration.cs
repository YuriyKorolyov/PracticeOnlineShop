using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()         
                .HasMaxLength(100);     

            builder.Property(p => p.Description)
                .HasMaxLength(500);    

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); 

            builder.Property(p => p.StockQuantity)
                .IsRequired();          

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(200);     

            builder.HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)       
                .HasForeignKey(pc => pc.ProductId); 
        }
    }
}
