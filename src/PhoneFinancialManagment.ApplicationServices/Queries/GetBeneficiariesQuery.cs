using MediatR;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.Queries;

public class GetBeneficiariesQuery : IRequest<IEnumerable<BenefeciaryDto>?>
{
    public int UserId { get; set; }
}