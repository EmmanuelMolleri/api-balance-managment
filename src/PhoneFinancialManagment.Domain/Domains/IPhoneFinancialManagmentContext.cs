using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Domain.Domains;

public interface IPhoneFinancialManagmentContext
{
    DbSet<User> Users { get; set; }
    DbSet<Beneficiaries> Beneficiaries { get; set; }
    DbSet<UserFinancialHistory> History { get; set; }
    DbSet<SystemConfiguration> SystemConfiguation { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}