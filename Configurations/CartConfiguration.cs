using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Quantity)
                .IsRequired(); 

            builder.HasOne(c => c.User)
                .WithMany(u => u.Carts)        
                .IsRequired();                  

            builder.HasOne(c => c.Product)
                .WithMany(p => p.Carts)         
                .IsRequired();                  
        }
    }
}
