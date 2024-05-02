using Application.Command;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Handlers.Article
{
    public sealed class AddArticleHandler : IRequestHandler<AddArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        public AddArticleHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            //_unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
           var entity = _mapper.Map<Domain.Entities.Article> (request);

            _articleRepository.Add(entity);

            return;
        }
    }
}
