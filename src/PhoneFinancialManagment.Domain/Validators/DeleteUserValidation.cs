using FluentValidation;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.Domain.Validators;

public class DeleteUserValidation : AbstractValidator<DeleteUserDto>
{
    public DeleteUserValidation(IUserDomainContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .Custom((id, validatorContext) =>
            {
                if (!context.Users.Any(x => x.UserId == id))
                {
                    validatorContext.AddFailure("The user is not found on our base.");
                }
            });
    }
}