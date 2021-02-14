using DevStore.Sales.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Catalogo.Data.Mappings
{
    public class OrderItemsMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CourseName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            // 1 : N => Order : OrderItems
            builder.HasOne(c => c.Order)
                .WithMany(c => c.OrderItems);

            builder.ToTable("OrderItems");
        }
    }
}