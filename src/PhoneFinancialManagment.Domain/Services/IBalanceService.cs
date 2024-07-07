namespace PhoneFinancialManagment.Domain.Services;

public interface IBalanceService
{
    Task<decimal> GetUserCurrentBalance(int userId, string token);
}
