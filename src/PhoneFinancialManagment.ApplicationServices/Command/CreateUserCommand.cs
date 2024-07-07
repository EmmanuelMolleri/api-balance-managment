using MediatR;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.Command;

public class CreateUserCommand: IRequest<UserDto?>
{
    public string Name { get; set; }
    public bool IsVerified { get; set; }
    public decimal Balance { get; set; }
    public IEnumerable<BenefeciaryDto> Beneficaries { get; set; } = new List<BenefeciaryDto>();
}