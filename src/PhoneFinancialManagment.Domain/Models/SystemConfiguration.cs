namespace PhoneFinancialManagment.Domain.Models;

public class SystemConfiguration
{
    public int ConfigurationId { get; set; }
    public int MaxBeneficiariesPerUser { get; set; }
    public int MaxBeneficiariesNicknameChars { get; set; }
    public decimal MaxAmountOfBalanceOfAllBeneficiaries { get; set; }
    public decimal IfVerifiedMaxDepositPerBeneficiariesPerMonth { get; set; }
    public decimal IfNotVerifiedMaxDepositPerBeneficiariesPerMonth { get; set; }
}