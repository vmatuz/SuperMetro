using Application.Repositories;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Handlers.Article;

public sealed class UpdateArticleHandler : IRequestHandler<UpdateArticleRequest, UpdateArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public UpdateArticleHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<UpdateArticleResponse> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
    {
        var articleEntity = await _articleRepository.Get(request.Id, cancellationToken);

        if (articleEntity != null)
        {
            articleEntity.Quantity = request.quantity;
            _articleRepository.Update(articleEntity);
            return new UpdateArticleResponse() { StatusCode = HttpStatusCode.OK };
        }
        else {
            return new UpdateArticleResponse() { StatusCode = HttpStatusCode.NotFound };
        }
    }
}

public sealed record UpdateArticleRequest(Guid Id, int quantity) : IRequest<UpdateArticleResponse>;

public sealed record UpdateArticleResponse
{
    public HttpStatusCode StatusCode{ get; set; }
}
