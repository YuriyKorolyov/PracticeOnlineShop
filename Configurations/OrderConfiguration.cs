using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Property(o => o.OrderDate)
                .IsRequired();  

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)")  
                .IsRequired();                  

            builder.Property(o => o.Status)
                .IsRequired();  

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)       
                .IsRequired();                  

            builder.HasOne(o => o.Payment)
                .WithOne(p => p.Order)          
                .IsRequired(false);             

            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order);       
        }
    }
}
