using PhoneFinancialManagment.Domain.Enums;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.DomainRules.Test;

public static class Constants
{
    public static User GetUser(int id = 1)
    {
        return new User
        {
            UserId = id,
            Name = "User Test",
            IsDeleted = false,
            IsUserVerified = false,
            Balance = 0
        };
    }

    public static Beneficiaries GetBeneficiary(int id = 1)
    {
        return new Beneficiaries
        {
            BeneficiaryId = id,
            Beneficiary = GetUser(id),
            UserId = id + 1,
            User = GetUser(id + 1)
        };
    }

    public static Beneficiaries GetBeneficiary(User mainUser, int beneficiaryId)
    {
        return new Beneficiaries
        {
            BeneficiaryId = beneficiaryId,
            Beneficiary = GetUser(beneficiaryId),
            UserId = mainUser.UserId,
            User = mainUser
        };
    }

    public static IEnumerable<Beneficiaries> GetBeneficiaries(User mainUser)
    {
        return new List<Beneficiaries>
        {
            GetBeneficiary(mainUser, 2),
            GetBeneficiary(mainUser, 3),
            GetBeneficiary(mainUser, 4),
            GetBeneficiary(mainUser, 5),
            GetBeneficiary(mainUser, 6)
        };
    }

    public static IEnumerable<PaymentQuantityOptions> GetPaymentQuantityOptions()
    {
        return new List<PaymentQuantityOptions>
        {
            new PaymentQuantityOptions
            {
                Id = 1,
                Value = 5
            },
            new PaymentQuantityOptions
            {
                Id = 2,
                Value = 10
            },
            new PaymentQuantityOptions
            {
                Id = 3,
                Value = 20
            },
            new PaymentQuantityOptions
            {
                Id = 4,
                Value = 30
            },
            new PaymentQuantityOptions
            {
                Id = 5,
                Value = 50
            },
            new PaymentQuantityOptions
            {
                Id = 6,
                Value = 75
            },
            new PaymentQuantityOptions
            {
                Id = 7,
                Value = 100
            },
        };
    }

    public static UserFinancialHistory GetUserFinancialHistory(User user)
    {
        return new UserFinancialHistory 
        {
            HistoryId = 1,
            Balance = user.Balance,
            DateRegistered = DateTime.Now,
            Type = HistoryType.AddBalance,
            UserId = user.UserId,
            User = user 
        };
    }

    public static SystemConfiguration GetSystemConfiguration()
    {
        return new SystemConfiguration
        {
            ConfigurationId = 1,
            IfNotVerifiedMaxDepositPerBeneficiariesPerMonth = 1000,
            IfVerifiedMaxDepositPerBeneficiariesPerMonth = 500,
            MaxAmountOfBalanceOfAllBeneficiaries = 2000,
            MaxBeneficiariesNicknameChars = 20,
            MaxBeneficiariesPerUser = 5
        };
    }
}