using FluentValidation;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.Domain.Validators;

public class RemoveBeneficiaryValidator : AbstractValidator<RemoveBeneficiaryDto>
{
    public RemoveBeneficiaryValidator(IUserDomainContext context)
    {
        RuleFor(x => x.BeneficiaryId)
            .NotEmpty()
            .NotNull()
            .Custom((id, validatorContext) =>
            {
                if (!context.Users.Any(x => x.UserId == id))
                {
                    validatorContext.AddFailure("The user is not found on our base.");
                    return;
                }
            });

        RuleFor(x => new { x.BeneficiaryId, x.UserId })
            .NotEmpty()
            .NotNull()
            .Custom((dto, validatorContext) =>
            {
                if (!context.Beneficiaries.Any(x => x.UserId == dto.UserId && x.BeneficiaryId == dto.BeneficiaryId))
                {
                    validatorContext.AddFailure("This beneficiary is not yours to delete.");
                }
            });
    }
}