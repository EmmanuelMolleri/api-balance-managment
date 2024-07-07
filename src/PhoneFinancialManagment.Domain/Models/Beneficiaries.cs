namespace PhoneFinancialManagment.Domain.Models;

public class Beneficiaries
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int BeneficiaryId { get; set; }
    public User Beneficiary { get; set; }
}