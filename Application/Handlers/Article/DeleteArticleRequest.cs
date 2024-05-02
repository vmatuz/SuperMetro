using Application.Command;
using Application.Command.Common;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Handlers.Article;

public sealed class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, StatusCodeResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public DeleteArticleHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }
    public async Task<StatusCodeResponse> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.Get(request.id, cancellationToken);

        if (article != null)
        {
            _articleRepository.Delete(article);

            return new StatusCodeResponse() { StatusCode = HttpStatusCode.OK };
        }

        return new StatusCodeResponse() { StatusCode = HttpStatusCode.NotFound };

    }
}
