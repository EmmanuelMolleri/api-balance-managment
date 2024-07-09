using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.Domain.Domains;

public interface IPaymentOptionsContext
{
    DbSet<PaymentQuantityOptions> PaymentQuantityOptions { get; set; }
}