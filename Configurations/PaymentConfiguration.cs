using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); 

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)") 
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .HasColumnType("timestamp") 
                .IsRequired(); 

            builder.Property(p => p.Status)
                .IsRequired();

            builder.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .IsRequired();

            builder.HasOne(p => p.PromoCode)
                .WithMany(pc => pc.Payments) 
                .HasForeignKey(p => p.PromoId)
                .IsRequired(false);
        }
    }
}
