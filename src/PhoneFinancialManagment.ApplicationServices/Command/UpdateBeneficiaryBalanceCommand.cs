using MediatR;
using System.Text.Json.Serialization;

namespace PhoneFinancialManagment.ApplicationServices.Command;

public class UpdateBeneficiaryBalanceCommand : IRequest<bool>
{
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public int BeneficiaryId { get; set; }
    public decimal Balance { get; set; }
}