namespace PhoneFinancialManagment.Domain.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public bool IsUserVerified { get; set; }
    public decimal Balance { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Beneficiaries> Beneficiaries = new List<Beneficiaries>();
    public ICollection<UserFinancialHistory> History = new List<UserFinancialHistory>();
}