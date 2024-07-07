using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.ApplicationServices.Queries;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.QueryHandler;

public class GetSpecificUserQueryHandler : IRequestHandler<GetSpecificUserQuery, UserDto?>
{
    private readonly IUserDomainContext _context;

    public GetSpecificUserQueryHandler(IUserDomainContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetSpecificUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .Include(x => x.Beneficiaries)
                .ThenInclude(x => x.Beneficiary)
            .FirstOrDefaultAsync(x => x.UserId == request.Id);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.UserId,
            Balance = user.Balance,
            Name = user.Name,
            IsVerified = user.IsUserVerified,
            Beneficaries = user.Beneficiaries.Select(beneficiary => new BenefeciaryDto
            {
                Id = beneficiary.BeneficiaryId,
                Balance = beneficiary.Beneficiary.Balance,
                Name = beneficiary.Beneficiary.Name,
                IsVerified = beneficiary.Beneficiary.IsUserVerified
            })
        };
    }
}