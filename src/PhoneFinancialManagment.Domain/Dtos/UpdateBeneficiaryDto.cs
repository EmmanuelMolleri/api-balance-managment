using System.Text.Json.Serialization;

namespace PhoneFinancialManagment.Domain.Dtos;

public class UpdateBeneficiaryDto
{
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public int BeneficiaryId { get; set; }
    [JsonIgnore]
    public string Token { get; set; }
    public decimal Balance { get; set; }
}