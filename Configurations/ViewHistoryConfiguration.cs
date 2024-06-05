using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Configurations
{
    public class ViewHistoryConfiguration : IEntityTypeConfiguration<ViewHistory>
    {
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
