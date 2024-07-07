using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.ApplicationServices.Queries;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.QueryHandler;

public class GetBeneficiariesQueryHandler : IRequestHandler<GetBeneficiariesQuery, IEnumerable<BenefeciaryDto>?>
{
    private readonly IPhoneFinancialManagmentContext _context;

    public async Task<IEnumerable<BenefeciaryDto>?> Handle(GetBeneficiariesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_context
            .Beneficiaries
                .Include(x => x.Beneficiary)
            .Where(x => x.UserId == request.UserId && !x.Beneficiary.IsDeleted).Select(x => new BenefeciaryDto
            {
                Id = x.Beneficiary.UserId,
                Balance = x.Beneficiary.Balance,
                Name = x.Beneficiary.Name,
                IsVerified = x.Beneficiary.IsUserVerified
            }));
    }
}