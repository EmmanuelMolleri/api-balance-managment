using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Infraestructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(table => table.UserId);

        builder
            .Property(table => table.Name)
            .HasColumnName("UserName")
            .HasColumnType("VARCHAR(60)")
            .IsRequired();

        builder
            .Property(table => table.IsUserVerified)
            .HasColumnName("IsUserVerified")
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder
            .Property(table => table.IsDeleted)
            .HasColumnName("IsDeleted")
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder
            .Property(table => table.Balance)
            .HasColumnName("Balance")
            .HasColumnType("DECIMAL")
            .HasDefaultValue(0);

        builder
            .HasMany(table => table.Beneficiaries)
            .WithOne(foreingTable => foreingTable.User)
            .HasForeignKey(table => table.UserId)
            .IsRequired();
    }
}