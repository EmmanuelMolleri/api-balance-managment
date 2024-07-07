using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoneFinancialManagment.ApplicationServices.Command;
using PhoneFinancialManagment.ApplicationServices.Queries;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Infraestructure.Security;

namespace PhoneFinancialManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles("Admin")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IValidator<NewUserDto> _newUserValidator { get; set; }
        private IValidator<UpdateUserDto> _updateUserValidator { get; set; }
        private IValidator<DeleteUserDto> _removeUserValidator { get; set; }

        public UserController(IMediator mediator,
            IValidator<NewUserDto> newUserValidator,
            IValidator<UpdateUserDto> updateUserValidator,
            IValidator<DeleteUserDto> removeUserValidator)
        {
            _mediator = mediator;
            _newUserValidator = newUserValidator;
            _updateUserValidator = updateUserValidator;
            _removeUserValidator = removeUserValidator;
        }

        /// <summary>
        /// query for a list of users and its benefiaries.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.Any())
            {
                return Ok(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Get specific user by Id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetSpecificUserQuery query)
        {
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return Ok(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Create a new user / costumer.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewUserDto payload)
        {
            var validatorResult = _newUserValidator.Validate(payload);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.ToString("~"));
            }

            var command = new CreateUserCommand
            {
                Name = payload.Name,
                Balance = payload.Balance,
                IsVerified = payload.IsVerified
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Update the user by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto payload)
        {
            payload.Id = id;
            var validatorResult = _updateUserValidator.Validate(payload);

            if (validatorResult.IsValid)
            {
                return BadRequest(validatorResult.ToString("~"));
            }

            var command = new UpdateUserCommand
            {
                Id = payload.Id,
                Name = payload.Name
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserDto payload)
        {
            var validatorResult = _removeUserValidator.Validate(payload);

            if (validatorResult.IsValid)
            {
                return NoContent();
            }

            var command = new DeleteUserCommand
            {
                Id = payload.Id,
            };

            var result = await _mediator.Send(command);

            return result ? Ok() : BadRequest();
        }
    }
}
