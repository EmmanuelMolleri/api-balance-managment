namespace PhoneFinancialManagment.Domain.Models;

public class UserClaims
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
}