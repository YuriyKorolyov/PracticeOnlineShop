using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MyApp.Configurations
{
    /// <summary>
    /// Конфигурация для сущности IdentityRole.
    /// </summary>
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
    {
        /// <summary>
        /// Настраивает сущность IdentityRole.
        /// </summary>
        /// <param name="builder">Строитель для сущности User.</param>
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            List<IdentityRole<int>> roles = new List<IdentityRole<int>>
            {
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.HasData(roles);
        }
    }
}
