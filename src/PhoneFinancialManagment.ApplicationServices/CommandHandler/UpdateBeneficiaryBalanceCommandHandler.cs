using MediatR;
using PhoneFinancialManagment.ApplicationServices.Command;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Enums;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.ApplicationServices.CommandHandler;

public class UpdateBeneficiaryBalanceCommandHandler : IRequestHandler<UpdateBeneficiaryBalanceCommand, bool>
{
    private readonly IPhoneFinancialManagmentContext _context;

    public UpdateBeneficiaryBalanceCommandHandler(IPhoneFinancialManagmentContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateBeneficiaryBalanceCommand request, CancellationToken cancellationToken)
    {
        var newLog = new UserFinancialHistory
        {
            Balance = request.Balance,
            Type = (request.Balance > 0) ? HistoryType.AddBalance : HistoryType.RemoveBalance,
            UserId = request.UserId,
            DateRegistered = DateTime.Now
        };

        var beneficiary = _context.Users.First(x => x.UserId == request.BeneficiaryId);
        beneficiary.Balance += request.Balance - 1;
        
        _context.Users.Update(beneficiary);
        _context.History.Add(newLog);

        int updatedRows = await _context.SaveChangesAsync();
        return updatedRows > 0;
    }
}