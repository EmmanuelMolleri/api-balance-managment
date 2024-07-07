using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhoneFinancialManagment.ApplicationServices.Command;
using PhoneFinancialManagment.ApplicationServices.Queries;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Infraestructure.Security;


namespace PhoneFinancialManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles("User")]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<NewBenefeciaryDto> _newBeneficiaryValidator;
        private readonly IValidator<UpdateBeneficiaryDto> _updateBlanceValidator;
        private readonly IValidator<RemoveBeneficiaryDto> _removeUserValidator;

        public BeneficiaryController(IMediator mediator,
            IValidator<NewBenefeciaryDto> newBeneficiaryValidator,
            IValidator<UpdateBeneficiaryDto> updateBlanceValidator,
            IValidator<RemoveBeneficiaryDto> removeUserValidator)
        {
            _mediator = mediator;
            _newBeneficiaryValidator = newBeneficiaryValidator;
            _updateBlanceValidator = updateBlanceValidator;
            _removeUserValidator = removeUserValidator;
        }

        /// <summary>
        /// It gets all your beneficiaries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetBeneficiariesQuery
            {
                UserId = int.Parse(HttpContext.Items["UserId"].ToString())
            };

            var result = await _mediator.Send(query);

            if (result.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Create a new beneficiary.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewBenefeciaryDto request)
        {
            request.UserId = int.Parse(HttpContext.Items["UserId"].ToString());

            var validationResult = _newBeneficiaryValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("~"));
            }

            var command = new NewBeneficiaryCommand
            {
                Name = request.Name,
                UserId = request.UserId,
                Balance = request.Balance,
                IsVerified = request.IsVerified
            };

            var result = await _mediator.Send(command);

            return Created("/", result);
        }

        /// <summary>
        /// Updates the current balance of a beneficiary.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBeneficiaryDto request)
        {
            request.UserId = int.Parse(HttpContext.Items["UserId"].ToString());
            request.BeneficiaryId = id;
            request.Token = HttpContext.Items["Token"].ToString();

            var validationResult = _updateBlanceValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("~"));
            }

            var command = new UpdateBeneficiaryBalanceCommand
            {
                UserId = request.UserId,
                BeneficiaryId = request.BeneficiaryId,
                Balance = request.Balance,
            };

            var result = await _mediator.Send(command);

            return Created("/", result);
        }

        /// <summary>
        /// Removes one of your own beneficiaries.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveBeneficiaryDto payload)
        {
            var validatorResult = _removeUserValidator.Validate(payload);

            if (validatorResult.IsValid)
            {
                return NoContent();
            }

            var command = new DeleteUserCommand
            {
                Id = payload.BeneficiaryId,
            };

            var result = await _mediator.Send(command);

            return result ? Ok() : BadRequest();
        }
    }
}
