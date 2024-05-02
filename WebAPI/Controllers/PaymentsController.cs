using Application.Command;
using Application.Handlers.Article;
using Application.Handlers.Payment;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers;
[ApiController]
[Route("payment")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ArticlesController> _logger;
    private readonly IValidator<AddArticleCommand> _validator;

    public PaymentsController(IMediator mediator, ILogger<ArticlesController> logger, IValidator<AddArticleCommand> validator)
    {
        _mediator = mediator;
        _logger = logger;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<GetPaymentResponse>> Get([FromQuery] string id, CancellationToken cancellationToken)
    {
        try
        {
            GetPaymentRequest request = new(Guid.Parse(id));

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
    public async Task<IActionResult> Update([FromQuery] string id, double amount, CancellationToken cancellationToken)
    {
        try
        {
            UpdatePaymentRequest request = new(Guid.Parse(id), amount);

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
    public async Task<IActionResult> Add([FromForm] AddPaymentCommand command, CancellationToken cancellationToken)
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
    public async Task<IActionResult> Delete([FromForm] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            DeletePaymentCommand dac = new(id);
            var result = await _mediator.Send(dac, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
