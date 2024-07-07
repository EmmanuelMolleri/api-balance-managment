using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Domain.Validators;

namespace PhoneFinancialManagment.Ioc;

public static class ValidatorInjection
{
    public static void Inject(IServiceCollection services)
    {
        services.AddScoped<IValidator<NewUserDto>, NewUserValidation>();
        services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
        services.AddScoped<IValidator<DeleteUserDto>, DeleteUserValidation>();
        services.AddScoped<IValidator<NewBenefeciaryDto>, NewBeneficiaryValidator>();
        services.AddScoped<IValidator<RemoveBeneficiaryDto>, RemoveBeneficiaryValidator>();
        services.AddScoped<IValidator<UpdateBeneficiaryDto>, UpdateBeneficiaryBalanceValidator>();
    }
}