using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneFinancialManagment.Domain.Services;
using PhoneFinancialManagment.Infraestructure.Services;

namespace PhoneFinancialManagment.Ioc;

public static class ServiceInjection
{
    public static void Inject(IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient<IBalanceService, BalanceService>(async (serviceProvider, client) =>
        {
            var secretsManagerService = serviceProvider.GetRequiredService<ISecretsManagerService>();
            string secretName = configuration.GetSection("BALANCE_API_URL_KEY").Value;
            string secretValue = await secretsManagerService.GetSecretAsync(secretName);

            client.BaseAddress = new Uri(secretValue);
        });
    }
}