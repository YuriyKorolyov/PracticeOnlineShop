﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Configurations
{
    /// <summary>
    /// Конфигурация для сущности Review.
    /// </summary>
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        /// <summary>
        /// Настраивает сущность Review.
        /// </summary>
        /// <param name="builder">Строитель для сущности Review.</param>
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.ReviewText)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .IsRequired();

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .IsRequired();
        }
    }

}
