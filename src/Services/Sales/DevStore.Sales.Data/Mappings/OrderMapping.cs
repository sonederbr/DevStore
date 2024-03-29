﻿using DevStore.Sales.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Catalogo.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .HasDefaultValueSql("NEXT VALUE FOR MySequence");

            // 1 : N => Order : OrderItems
            builder.HasMany(c => c.OrderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId);

            builder.ToTable("Orders");
        }
    }
}