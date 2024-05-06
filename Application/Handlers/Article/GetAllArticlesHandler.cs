using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Handlers.Article
{
    public sealed class GetAllArticlesHandler : IRequestHandler<GetAllArticlesRequest, List<GetArticleResponse>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetAllArticlesHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            //_unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetArticleResponse>> Handle(GetAllArticlesRequest request, CancellationToken cancellationToken)
        {
           var articles=  _articleRepository.GetAllArticles(cancellationToken);

            var articlesResponse =   _mapper.Map<List<GetArticleResponse>>(articles);

            return articlesResponse;
        }
    }

    public sealed record GetAllArticlesRequest(): IRequest<List<GetArticleResponse>>;
}
