using MediatR;
using PhoneFinancialManagment.ApplicationServices.Command;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.ApplicationServices.CommandHandler;

public class NewBeneficiaryCommandHandler : IRequestHandler<NewBeneficiaryCommand, BenefeciaryDto>
{
    private readonly IPhoneFinancialManagmentContext _context;

    public NewBeneficiaryCommandHandler(IPhoneFinancialManagmentContext context)
    {
        _context = context;
    }

    public async Task<BenefeciaryDto> Handle(NewBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var dependent = new User
        {
            Name = request.Name,
            IsUserVerified = request.IsVerified,
            Balance = request.Balance
        };

        var beneficiary = new Beneficiaries
        {
            UserId = request.UserId,
            Beneficiary = dependent
        };

        _context.Users.Add(dependent);
        _context.Beneficiaries.Add(beneficiary);

        await _context.SaveChangesAsync();

        return new BenefeciaryDto
        {
            Id = dependent.UserId,
            Name = dependent.Name,
            Balance = dependent.Balance,
            IsVerified = dependent.IsUserVerified
        };
    }
}