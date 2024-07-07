namespace PhoneFinancialManagment.Domain.Dtos;

public class BenefeciaryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public bool IsVerified { get; set; }
}