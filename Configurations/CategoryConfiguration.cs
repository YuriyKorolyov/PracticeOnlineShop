using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.CategoryName)
                .IsRequired()                 
                .HasMaxLength(100);        

            builder.HasMany(c => c.ProductCategories)
                .WithOne(pc => pc.Category)    
                .HasForeignKey(pc => pc.CategoryId)   
                .IsRequired();                    
        }
    }
}
