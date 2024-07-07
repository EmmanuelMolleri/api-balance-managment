using PhoneFinancialManagment.Domain.Services;
using System.Net.Http.Headers;

namespace PhoneFinancialManagment.Infraestructure.Services;

public class BalanceService : IBalanceService
{
    private readonly HttpClient _httpClient;

    public BalanceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetUserCurrentBalance(int userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/GetUserBalance?userId={userId}");

        if (response.IsSuccessStatusCode)
        {
            return decimal.Parse(await response.Content.ReadAsStringAsync());
        }
        else
        {
            throw new Exception("Error calling external API");
        }
    }
}