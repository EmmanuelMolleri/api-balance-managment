using System.Text.Json.Serialization;

namespace PhoneFinancialManagment.Domain.Dtos;

public class NewBenefeciaryDto
{
    [JsonIgnore]
    public int UserId { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public bool IsVerified { get; set; }
}