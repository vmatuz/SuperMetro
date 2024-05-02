using MediatR;
using AutoMapper;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.Handlers.Article;

public sealed class GetArticleHandler : IRequestHandler<GetArticleRequest, GetArticleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<GetArticleResponse> Handle(GetArticleRequest request, CancellationToken cancellationToken)
    {
        var articleEntity = await _articleRepository.Get(request.Id,cancellationToken);

        // await _unitOfWork.Save(cancellationToken);

        return _mapper.Map<GetArticleResponse>(articleEntity);
    }
}

public sealed record GetArticleRequest(Guid Id) : IRequest<GetArticleResponse>;

public sealed record GetArticleResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool AllowDiscount { get; set; }
    public int BatchNumber { get; set; }
    public ArticleTypes Type { get; set; }
    public ICollection<ArticleTransaction> ArticleTransactions { get; set; }
}
