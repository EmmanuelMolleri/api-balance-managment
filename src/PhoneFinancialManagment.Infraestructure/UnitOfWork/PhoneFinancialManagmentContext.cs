using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Models;
using PhoneFinancialManagment.Infraestructure.Configuration;

namespace PhoneFinancialManagment.Infraestructure.UnitOfWork;

public class PhoneFinancialManagmentContext: DbContext, IUserDomainContext, IPhoneFinancialManagmentContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Beneficiaries> Beneficiaries { get; set; }
    public DbSet<UserFinancialHistory> History { get; set; }
    public DbSet<SystemConfiguration> SystemConfiguation { get; set; }

    public PhoneFinancialManagmentContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}