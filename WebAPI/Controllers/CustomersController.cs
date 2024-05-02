using Application.Command;
using Application.Command.Common;
using Application.Handlers.Customer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomersController> _logger;
        private readonly IValidator<AddCustomerCommand> _validator;

        public CustomersController(IMediator mediator, ILogger<CustomersController> logger, IValidator<AddCustomerCommand> validator)
        {
            _mediator = mediator;
            _logger = logger;
            _validator = validator;
        }
        /// <summary>
        /// IAmTrevor@myEmail.com
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GetCustomerResponse>> Get([FromQuery] string email, CancellationToken cancellationToken)
        {
            try
            {
                GetCustomerRequest request = new(email);

                var response = await _mediator.Send(request, cancellationToken);

                if (response == null) return StatusCode(404);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromQuery] string id, string address, CancellationToken cancellationToken)
        {
            try
            {
                UpdateCustomerRequest request = new(Guid.Parse(id), address);

                var response = await _mediator.Send(request, cancellationToken);

                return StatusCode((int)response.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromForm] AddCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(command, cancellationToken);

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromForm] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                DeleteCustomerCommand dac = new(id);
                var result = await _mediator.Send(dac, cancellationToken);

                return StatusCode((int)result.StatusCode);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
