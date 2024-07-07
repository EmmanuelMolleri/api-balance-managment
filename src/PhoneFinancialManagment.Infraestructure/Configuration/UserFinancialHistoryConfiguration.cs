using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Infraestructure.Configuration;

public class UserFinancialHistoryConfiguration : IEntityTypeConfiguration<UserFinancialHistory>
{
    public void Configure(EntityTypeBuilder<UserFinancialHistory> builder)
    {
        builder
            .ToTable("UserFinancialHistory");

        builder
            .HasKey(table => table.HistoryId)
            .HasName("UserFinancialHistoryId");

        builder
            .Property(table => table.Type)
            .HasColumnType("INT")
            .HasColumnName("HistoryType")
            .IsRequired();

        builder
            .HasOne(table => table.User)
            .WithMany(userTable => userTable.History)
            .HasForeignKey(table => table.UserId);

        builder
            .Property(table => table.Balance)
            .HasColumnType("Decimal(10, 2)")
            .HasColumnName("Balance")
            .IsRequired();

        builder
            .Property(table => table.DateRegistered)
            .HasColumnName("DateRegistered")
            .HasColumnType("DateTime")
            .HasDefaultValue("GETDATE()");
    }
}