using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Domain.Models;
using PhoneFinancialManagment.Domain.Validators;

namespace PhoneFinancialManagment.DomainRules.Test
{
    public class BeneficiaryTests
    {
        private readonly NewBeneficiaryValidator _validator;
        public readonly Mock<IPhoneFinancialManagmentContext> _context = new();
        private readonly NewBenefeciaryDto _dto = new NewBenefeciaryDto
        {
            UserId = 1,
            Balance = 0,
            IsVerified = true,
            Name = "Test"
        };

        public BeneficiaryTests()
        {
            _context.Setup(x => x.SystemConfiguation).ReturnsDbSet(new List<SystemConfiguration> { Constants.GetSystemConfiguration() });
            _validator = new NewBeneficiaryValidator(_context.Object);
        }

        [Theory(DisplayName = "Validate max beneficiaries quantity")]
        [InlineData()]
        public void ValidateMaxBeneficiaries()
        {
            _context.Setup(x => x.Users).ReturnsDbSet(new List<User> { Constants.GetUser() });
            _context.Setup(x => x.Beneficiaries).ReturnsDbSet(Constants.GetBeneficiaries(Constants.GetUser()));
            
            var result = _validator.Validate(_dto);

            Assert.False(result.IsValid);
            Assert.Contains("You can't add a new beneficiary because you hit the max quantity of beneficiaries.", result.ToString("~"));
        }

        [Theory(DisplayName = "Validate beneficiaries max char quantity")]
        [InlineData("New name with more than twenty characters :D")]
        public void ValidateBeneficiaryNameMaxQuantity(string newName)
        {
            _dto.Name = newName;
            _dto.Balance = 50;

            var user = Constants.GetUser();
            user.Balance = 5000;

            _context.Setup(x => x.Users).ReturnsDbSet(new List<User> { user });
            
            var beneficiaries = Constants.GetBeneficiaries(Constants.GetUser()).ToList();
            beneficiaries.Remove(beneficiaries.First());
            
            _context.Setup(x => x.Beneficiaries).ReturnsDbSet(beneficiaries);

            var result = _validator.Validate(_dto);

            Assert.False(result.IsValid);
            Assert.Contains("Your beneficiary can't have more than 20 characters.", result.ToString("~"));
        }

        [Theory(DisplayName = "Validate Max balance amount")]
        [InlineData(50000)]
        public void ValidateBeneficiaryWithMaxBalanceAmount(decimal balance)
        {
            _dto.Balance = balance;

            _context.Setup(x => x.Users).ReturnsDbSet(new List<User> { Constants.GetUser() });

            var beneficiaries = Constants.GetBeneficiaries(Constants.GetUser()).ToList();
            beneficiaries.Remove(beneficiaries.First());

            _context.Setup(x => x.Beneficiaries).ReturnsDbSet(beneficiaries);
            var result = _validator.Validate(_dto);

            Assert.False(result.IsValid);
            Assert.Contains("The balance that your beneficiary set, exceeds the limit of all your beneficiaries limit.", result.ToString("~"));
        }

        [Theory(DisplayName = "Validate Max balance amount if verified")]
        [InlineData(50000, true)]
        [InlineData(50000, false)]
        public void ValidateBeneficiaryVerifiedValidationWithMaxBalanceAmount(decimal balance, bool isValidated)
        {
            _dto.Balance = balance;
            _dto.IsVerified = isValidated;

            _context.Setup(x => x.Users).ReturnsDbSet(new List<User> { Constants.GetUser() });

            var beneficiaries = Constants.GetBeneficiaries(Constants.GetUser()).ToList();
            beneficiaries.Remove(beneficiaries.First());

            _context.Setup(x => x.Beneficiaries).ReturnsDbSet(beneficiaries);
            var result = _validator.Validate(_dto);

            Assert.False(result.IsValid);
            Assert.Contains("You can't add this amount of money to this beneficiary.", result.ToString("~"));
        }
    }
}