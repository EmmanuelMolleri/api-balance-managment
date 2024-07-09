using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Services;
using PhoneFinancialManagment.Infraestructure.UnitOfWork;

namespace PhoneFinancialManagment.Ioc;

public static class DatabaseInjection
{
    public static void Inject(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PhoneFinancialManagmentContext>(async (serviceProvider, options) =>
        {
            var secretsManagerService = serviceProvider.GetRequiredService<ISecretsManagerService>();
            string secretName = configuration.GetSection("DATABASE_CONNECTION_STRING_KEY").Value; 
            string connectionString = await secretsManagerService.GetSecretAsync(secretName);

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUserDomainContext>(serviceProvider => serviceProvider.GetService<PhoneFinancialManagmentContext>());
        services.AddScoped<IPhoneFinancialManagmentContext>(serviceProvider => serviceProvider.GetService<PhoneFinancialManagmentContext>());
        services.AddScoped<IPaymentOptionsContext>(serviceProvider => serviceProvider.GetService<PhoneFinancialManagmentContext>());
    }
}