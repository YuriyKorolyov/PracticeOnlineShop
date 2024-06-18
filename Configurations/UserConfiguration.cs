using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Configurations
{
    /// <summary>
    /// Конфигурация для сущности User.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Настраивает сущность User.
        /// </summary>
        /// <param name="builder">Строитель для сущности User.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired()           
                .HasMaxLength(50);     

            builder.Property(u => u.LastName)
                .IsRequired()           
                .HasMaxLength(50);      

            builder.Property(u => u.ShippingAddress)
                .HasMaxLength(200);     

            builder.Property(u => u.RegistrationDate)
                .HasColumnType("date");               
        }
    }
}
