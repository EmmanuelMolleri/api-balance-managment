using FluentValidation;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.Domain.Validators;

public class NewUserValidation : AbstractValidator<NewUserDto>
{
    public NewUserValidation()
    {
        RuleFor(x => x.Name.Trim())
            .NotEmpty()
            .WithMessage("The name of the user can't be empty.");

        RuleFor(x => x.Balance)
            .LessThan(0)
            .WithMessage("The balance of the user can't be negative");
    }
}