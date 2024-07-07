using FluentValidation;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.Domain.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator(IUserDomainContext context)
    {
        RuleFor(x => x.Id)
            .LessThanOrEqualTo(0)
            .WithMessage("The user doesn't exists")
            .Custom((userId, validatorContext) =>
            {
                if (!context.Users.Any(x => x.UserId == userId))
                {
                    validatorContext.AddFailure("The current user is not on our databases.");
                }
            });

        RuleFor(x => x.Name.Trim())
            .NotEmpty()
            .WithMessage("The name of the user can't be empty.");
    }
}