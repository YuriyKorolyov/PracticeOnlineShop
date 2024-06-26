﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Models;

namespace MyApp.Configurations
{
    /// <summary>
    /// Конфигурация для сущности OrderDetail.
    /// </summary>
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        /// <summary>
        /// Настраивает сущность OrderDetail.
        /// </summary>
        /// <param name="builder">Строитель для сущности OrderDetail.</param>
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id);

            builder.Property(od => od.Id).ValueGeneratedOnAdd();
            builder.Property(od => od.Quantity).IsRequired();
            builder.Property(od => od.UnitPrice).IsRequired().HasColumnType("decimal(18, 2)"); 

            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails);

            builder.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails);
        }
    }
}
