using MediatR;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.Command;

public class NewBeneficiaryCommand : IRequest<BenefeciaryDto>
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public bool IsVerified { get; set; }
}