using Microsoft.AspNetCore.Mvc;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Models;
using PhoneFinancialManagment.Infraestructure.Security;

namespace PhoneFinancialManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles("User", "Admin")]
    public class PaymentOptionsController : ControllerBase
    {
        private readonly IPaymentOptionsContext _context;

        public PaymentOptionsController(IPaymentOptionsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<PaymentQuantityOptions> Get()
        {
            return _context.PaymentQuantityOptions;
        }
    }
}
