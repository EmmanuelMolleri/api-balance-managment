using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Domain.Domains;

public interface IUserDomainContext
{
    DbSet<User> Users { get; set; }
    DbSet<Beneficiaries> Beneficiaries { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}