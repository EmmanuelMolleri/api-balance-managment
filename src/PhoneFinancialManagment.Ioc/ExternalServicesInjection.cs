using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneFinancialManagment.Domain.Services;
using PhoneFinancialManagment.Infraestructure.Security;
using PhoneFinancialManagment.Infraestructure.Services;
using StackExchange.Redis;

namespace PhoneFinancialManagment.Ioc;

public static class ExternalServicesInjection
{
    public static void Inject(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonSecretsManager>();
        services.AddSingleton<ISecretsManagerService, SecretsManagerService>();

        services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            var secretsManagerService = serviceProvider.GetRequiredService<ISecretsManagerService>();
            string secretName = configuration.GetSection("REDIS_CONNECTION").Value;
            var redisConnectionString = secretsManagerService.GetSecretAsync(secretName).GetAwaiter().GetResult();

            return ConnectionMultiplexer.Connect(redisConnectionString);
        });

        services.AddSingleton<IAuthenticationService, AuthenticationService>();

        services.AddScoped<AuthorizeRolesAttribute>(serviceProvider =>
        {
            var secretsManagerService = serviceProvider.GetRequiredService<ISecretsManagerService>();
            string secretName = configuration.GetSection("USER_ROLES_KEY").Value;

            var rolesSecret = secretsManagerService.GetSecretAsync(secretName).GetAwaiter().GetResult();
            var roles = rolesSecret.Split(',');

            return new AuthorizeRolesAttribute(roles);
        });
    }
}