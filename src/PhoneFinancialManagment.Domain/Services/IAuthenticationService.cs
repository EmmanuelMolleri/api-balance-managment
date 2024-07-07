using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Domain.Services;

public interface IAuthenticationService
{
    UserClaims ValidateTokenAndGetUserClaims(string token);
}