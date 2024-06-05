using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Configurations
{
    /// <summary>
    /// Конфигурация для сущности ViewHistory.
    /// </summary>
    public class ViewHistoryConfiguration : IEntityTypeConfiguration<ViewHistory>
    {
        /// <summary>
        /// Настраивает сущность ViewHistory.
        /// </summary>
        /// <param name="builder">Строитель для сущности ViewHistory.</param>
        public void Configure(EntityTypeBuilder<ViewHistory> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(v => v.ViewDate)
                   .HasColumnType("date")
                   .IsRequired(); 

            builder.HasOne(v => v.User) 
                   .WithMany(u => u.ViewHistories) 
                   .IsRequired(); 

            builder.HasOne(v => v.Product) 
                   .WithMany(p => p.ViewHistories) 
                   .IsRequired(); 
        }
    }
}
