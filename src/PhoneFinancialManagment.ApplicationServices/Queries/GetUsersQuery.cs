using MediatR;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.Queries;

public class GetUsersQuery : IRequest<IEnumerable<UserDto>?>
{
    public int Page { get; set; } = 0;
    public int PageQuantity { get; set; } = 50;

    public bool VerifiedUsersOnly { get; set; } = false;
    public string? Name { get; set; }
    public decimal? MinBalance { get; set; }
    public decimal? MaxBalance { get; set;}
}