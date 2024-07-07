using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.ApplicationServices.Queries;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.ApplicationServices.QueryHandler;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>?>
{
    private readonly IUserDomainContext _context;

    public GetUsersQueryHandler(IUserDomainContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>?> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> query = _context.Users
            .Include(x => x.Beneficiaries)
                .ThenInclude(x => x.Beneficiary)
            .Where(x => !x.IsDeleted);

        if (request.VerifiedUsersOnly)
        {
            query = query.Where(user => user.IsUserVerified);
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(user => user.Name.Contains(request.Name));
        }

        if (request.MinBalance.HasValue)
        {
            query = query.Where(user => user.Balance >= request.MinBalance);
        }

        if (request.MaxBalance.HasValue)
        {
            query = query.Where(user => user.Balance <= request.MaxBalance);
        }

        query.Skip(request.PageQuantity * request.Page).Take(request.PageQuantity);

        return query
            .Select(user => new UserDto
            {
                Id = user.UserId,
                Name = user.Name,
                IsVerified = user.IsUserVerified,
                Balance = user.Balance,
                Beneficaries = user.Beneficiaries
                    .Select(beneficiary =>
                        new BenefeciaryDto
                        {
                            Id = beneficiary.BeneficiaryId,
                            Name = beneficiary.Beneficiary.Name,
                            Balance = beneficiary.Beneficiary.Balance,
                            IsVerified = beneficiary.Beneficiary.IsUserVerified
                        })
            });
    }
}
