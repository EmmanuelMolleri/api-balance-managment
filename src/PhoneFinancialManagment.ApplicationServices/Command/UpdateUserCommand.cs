using MediatR;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.Command;

public class UpdateUserCommand : IRequest<UserDto?>
{
    public int Id { get; set; }
    public string Name { get; set; }
}