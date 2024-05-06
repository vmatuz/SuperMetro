using Application.Command;
using Application.Handlers.Article;
using Application.Handlers.Transaction;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers;

[ApiController]
[Route("transaction")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransactionsController> _logger;
    private readonly IValidator<AddTransactionCommand> _validator;

    public TransactionsController(IMediator mediator, ILogger<TransactionsController> logger, IValidator<AddTransactionCommand> validator)
    {
        _mediator = mediator;
        _logger = logger;
        _validator = validator;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<GetAllTransactionsResponse>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            GetAllTransactionsRequest request = new();
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddTransactionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}

