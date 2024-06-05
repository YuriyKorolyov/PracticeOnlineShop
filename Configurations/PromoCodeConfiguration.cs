using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Configurations
{
    /// <summary>
    /// Конфигурация для сущности PromoCode.
    /// </summary>
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        /// <summary>
        /// Настраивает сущность PromoCode.
        /// </summary>
        /// <param name="builder">Строитель для сущности PromoCode.</param>
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(pc => pc.PromoName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pc => pc.Discount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(pc => pc.StartDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(pc => pc.EndDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.HasMany(pc => pc.Payments)
                   .WithOne(p => p.PromoCode) 
                   .HasForeignKey(p => p.PromoId);
        }
    }
}
