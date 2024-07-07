using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Infraestructure.Configuration;

public class SystemConfigurationConfiguration : IEntityTypeConfiguration<SystemConfiguration>
{
    public void Configure(EntityTypeBuilder<SystemConfiguration> builder)
    {
        builder
            .ToTable("SystemConfigurations");

        builder
            .HasKey(table => table.ConfigurationId);

        builder
            .Property(table => table.MaxBeneficiariesPerUser)
            .HasColumnName("MaxBeneficiariesPerUser")
            .HasColumnType("INT")
            .HasDefaultValue("5");

        builder
            .Property(table => table.MaxAmountOfBalanceOfAllBeneficiaries)
            .HasColumnName("MaxAmountOfBalanceOfAllBeneficiaries")
            .HasColumnType("Decimal(10, 2)")
            .HasDefaultValue("3000");

        builder
            .Property(table => table.IfVerifiedMaxDepositPerBeneficiariesPerMonth)
            .HasColumnName("IfVerifiedMaxDepositPerBeneficiaries")
            .HasColumnType("Decimal(10, 2)")
            .HasDefaultValue("1000");

        builder
            .Property(table => table.IfNotVerifiedMaxDepositPerBeneficiariesPerMonth)
            .HasColumnName("IfNotVerifiedMaxDepositPerBeneficiaries")
            .HasColumnType("Decimal(10, 2)")
            .HasDefaultValue("500");

        builder
            .Property(table => table.MaxBeneficiariesNicknameChars)
            .HasColumnName("MaxBeneficiariesNicknameChars")
            .HasColumnType("INT")
            .HasDefaultValue("20");
    }
}