using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Domain.Services;

namespace PhoneFinancialManagment.Domain.Validators;

public class UpdateBeneficiaryBalanceValidator : AbstractValidator<UpdateBeneficiaryDto>
{
    public UpdateBeneficiaryBalanceValidator(IPhoneFinancialManagmentContext context, IBalanceService balanceService)
    {
        var systemConfiguration = context.SystemConfiguation.First();

        RuleFor(x => new { x.UserId, x.BeneficiaryId, x.Balance })
            .Custom((dto, validator) =>
            {
                var mainUser = context.Users.First(x => x.UserId == dto.UserId);

                if (!context.Beneficiaries.Any(x => x.BeneficiaryId == dto.BeneficiaryId && x.UserId == dto.UserId) ||
                    context.Users.First(x => x.UserId == dto.BeneficiaryId).IsDeleted)
                {
                    validator.AddFailure("This beneficiary doesn't belong to you.");
                    return;
                }

                var currentFlowOfBalance = context
                .Users
                    .FirstOrDefault(x => x.UserId == dto.BeneficiaryId)?
                .History
                    .Where(x => x.DateRegistered.Month == DateTime.Now.Month &&
                    x.DateRegistered.Year == DateTime.Now.Year)
                .Sum(x => x.Balance);

                if ((currentFlowOfBalance + dto.Balance > systemConfiguration.IfVerifiedMaxDepositPerBeneficiariesPerMonth && mainUser.IsUserVerified) ||
                    (currentFlowOfBalance + dto.Balance > systemConfiguration.IfNotVerifiedMaxDepositPerBeneficiariesPerMonth && !mainUser.IsUserVerified))
                {
                    validator.AddFailure("You can't add this amount of money to this beneficiary.");
                }

                if (systemConfiguration.MaxAmountOfBalanceOfAllBeneficiaries > (context.Beneficiaries.Include(x => x.Beneficiary).Where(x => x.UserId == dto.UserId).Sum(x => x.Beneficiary.Balance) + dto.Balance))
                {
                    validator.AddFailure("The balance that your beneficiary set, exceeds the limit of all your beneficiaries limit.");
                }

                if ((mainUser.IsUserVerified && systemConfiguration.IfVerifiedMaxDepositPerBeneficiariesPerMonth < dto.Balance) ||
                    ((!mainUser.IsUserVerified) && systemConfiguration.IfNotVerifiedMaxDepositPerBeneficiariesPerMonth < dto.Balance))
                {
                    validator.AddFailure("You can't add this amount of money to this beneficiary.");
                }
            });

        RuleFor(x => new { x.Balance, x.Token, x.UserId })
            .Custom((dto, validator) =>
            {
                var mainUserCurrentBalance = balanceService.GetUserCurrentBalance(dto.UserId, dto.Token).Result;
                if (mainUserCurrentBalance < dto.Balance + 1)
                {
                    validator.AddFailure("You don't have this amount of money on balance to share with this beneficiary.");
                }
            });
    }
}