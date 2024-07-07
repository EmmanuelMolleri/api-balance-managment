using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Infraestructure.Configuration;

public class BeneficiariesConfiguration : IEntityTypeConfiguration<Beneficiaries>
{
    public void Configure(EntityTypeBuilder<Beneficiaries> builder)
    {
        builder
            .ToTable("Beneficiaries");

        builder
            .HasKey(table => new { table.UserId, table.BeneficiaryId });

        builder
            .HasOne(table => table.User)
            .WithMany(table => table.Beneficiaries)
            .HasForeignKey(user => user.UserId);

        builder
            .HasOne(table => table.Beneficiary)
            .WithMany()
            .HasForeignKey(table => table.BeneficiaryId);
    }
}