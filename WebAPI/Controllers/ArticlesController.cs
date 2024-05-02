using Application.Command;
using Application.Handlers.Article;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers;

[ApiController]
[Route("article")]
public class ArticlesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ArticlesController> _logger;
    private readonly IValidator<AddArticleCommand> _validator;

    public ArticlesController(IMediator mediator, ILogger<ArticlesController> logger, IValidator<AddArticleCommand> validator)
    {
        _mediator = mediator;
        _logger = logger;
        _validator = validator;
    }

    /// <summary>
    /// 8b441c77-604a-44c0-9550-a739dbf06000
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<GetArticleResponse>> Get([FromQuery] string id, CancellationToken cancellationToken)
    {
        try
        {
            GetArticleRequest request = new(Guid.Parse(id));

            var response = await _mediator.Send(request, cancellationToken);

            if (response == null) return StatusCode(404);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpGet]
    [Route("getAllArticles")]
    public async Task<ActionResult<GetArticleResponse>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            GetAllArticlesRequest request = new();
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] string id, int quantity, CancellationToken cancellationToken)
    {
        try
        {
            UpdateArticleRequest request = new(Guid.Parse(id), quantity);

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
    public async Task<IActionResult> Add([FromForm] AddArticleCommand command, CancellationToken cancellationToken)
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
            DeleteArticleCommand dac = new(id);
            var result = await _mediator.Send(dac, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}

