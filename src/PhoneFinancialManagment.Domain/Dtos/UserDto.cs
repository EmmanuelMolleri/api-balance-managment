namespace PhoneFinancialManagment.Domain.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsVerified { get; set; }
    public decimal Balance { get; set; }
    public IEnumerable<BenefeciaryDto> Beneficaries { get; set; } = new List<BenefeciaryDto>();
}