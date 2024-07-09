using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Infraestructure.Configuration;

public class PaymentQuantityOptionsConfiguration : IEntityTypeConfiguration<PaymentQuantityOptions>
{
    public void Configure(EntityTypeBuilder<PaymentQuantityOptions> builder)
    {
        builder
            .ToTable("PaymentOptions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .HasColumnName("PaymentValue")
            .HasColumnType("Decimal(10, 2)");
    }
}