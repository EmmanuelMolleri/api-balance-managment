namespace PhoneFinancialManagment.Domain.Services;

public interface ISecretsManagerService
{
    Task<string> GetSecretAsync(string secretName);
}