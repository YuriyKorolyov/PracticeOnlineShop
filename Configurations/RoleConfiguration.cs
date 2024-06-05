using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.RoleName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(r => r.Users) 
                   .WithOne(u => u.Role)
                   .IsRequired();

            builder.HasData(
            new Role { Id = 1, RoleName = "User" },
            new Role { Id = 2, RoleName = "Admin" });
        }
    }
}
