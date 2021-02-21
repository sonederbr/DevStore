using DevStore.Finance.Business;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Finance.Data.Mappings
{
    public class PaymentMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameCard)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.NumberCard)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(c => c.ExpirationDateCard)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.CvvCard)
                .IsRequired()
                .HasColumnType("varchar(4)");

            // 1 : 1 => Pagamento : Transacao
            builder.HasOne(c => c.Transaction)
                .WithOne(c => c.Payment);

            builder.ToTable("Payments");
        }
    }
}