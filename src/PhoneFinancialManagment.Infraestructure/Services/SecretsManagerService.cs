using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using PhoneFinancialManagment.Domain.Services;

namespace PhoneFinancialManagment.Infraestructure.Services;

public class SecretsManagerService : ISecretsManagerService
{
    private readonly IAmazonSecretsManager _secretsManager;

    public SecretsManagerService(IAmazonSecretsManager secretsManager)
    {
        _secretsManager = secretsManager;
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        var request = new GetSecretValueRequest
        {
            SecretId = secretName
        };

        var response = await _secretsManager.GetSecretValueAsync(request);

        if (response.SecretString != null)
        {
            return response.SecretString;
        }
        else
        {
            using (var memoryStream = response.SecretBinary)
            {
                using (var reader = new StreamReader(memoryStream))
                {
                    return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                }
            }
        }
    }
}
