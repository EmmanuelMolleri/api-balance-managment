namespace PhoneFinancialManagment.Domain.Dtos;

public class NewUserDto
{
    public string Name { get; set; }
    public bool IsVerified { get; set; }
    public decimal Balance { get; set; }
}