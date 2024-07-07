using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.Domain.Validators;

public class NewBeneficiaryValidator : AbstractValidator<NewBenefeciaryDto>
{
    public NewBeneficiaryValidator(IPhoneFinancialManagmentContext context)
    {
        var systemConfiguration = context.SystemConfiguation.First();

        RuleFor(x => x.UserId)
            .Custom((userId, validationContext) =>
            {
                if (systemConfiguration.MaxBeneficiariesPerUser <= context.Beneficiaries.Include(x => x.Beneficiary).Count(x => x.UserId == userId && !x.Beneficiary.IsDeleted))
                {
                    validationContext.AddFailure("You can't add a new beneficiary because you hit the max quantity of beneficiaries.");
                }
            });

        RuleFor(x => x.Name)
            .Custom((userName, validationContext) =>
            {
                if (systemConfiguration.MaxBeneficiariesNicknameChars > userName.Length)
                {
                    validationContext.AddFailure($"Your beneficiary can't have more than {systemConfiguration.MaxBeneficiariesNicknameChars} characters.");
                }
            });

        RuleFor(x => x.Balance)
            .LessThan(0)
            .WithMessage("You can't define a negative balance.");

        RuleFor(x => new { x.Balance, x.UserId })
            .Custom((newBeneficiary, validationContext) =>
            {
                var mainUser = context.Users.First(x => x.UserId == newBeneficiary.UserId);
                if (systemConfiguration.MaxAmountOfBalanceOfAllBeneficiaries > (context.Beneficiaries.Include(x => x.Beneficiary).Where(x => x.UserId == newBeneficiary.UserId).Sum(x => x.Beneficiary.Balance) + newBeneficiary.Balance))
                {
                    validationContext.AddFailure("The balance that your beneficiary set, exceeds the limit of all your beneficiaries limit.");
                }

                if ((mainUser.IsUserVerified && systemConfiguration.IfVerifiedMaxDepositPerBeneficiariesPerMonth < newBeneficiary.Balance) ||
                    ((!mainUser.IsUserVerified) && systemConfiguration.IfNotVerifiedMaxDepositPerBeneficiariesPerMonth < newBeneficiary.Balance))
                {
                    validationContext.AddFailure("You can't add this amount of money to this beneficiary.");
                }
            });
    }
}