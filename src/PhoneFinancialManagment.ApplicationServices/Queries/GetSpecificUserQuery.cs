using MediatR;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.Queries;

public class GetSpecificUserQuery: IRequest<UserDto?>
{
    public int Id { get; set; }
}