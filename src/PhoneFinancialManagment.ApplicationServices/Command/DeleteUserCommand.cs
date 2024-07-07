using MediatR;

namespace PhoneFinancialManagment.ApplicationServices.Command;

public class DeleteUserCommand: IRequest<bool>
{
    public int Id { get; set; }
}