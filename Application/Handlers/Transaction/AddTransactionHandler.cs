using Application.Command;
using Application.Command.Common;
using Application.Handlers.Article;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Handlers.Transaction;

public sealed class AddTransactionHandler : IRequestHandler<AddTransactionCommand, StatusCodeResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public AddTransactionHandler(ITransactionRepository transactionRepository, IArticleRepository articleRepository,
        IMapper mapper)
    {
       // _unitOfWork = unitOfWork;
        _transactionRepository = transactionRepository;
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<StatusCodeResponse> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Transaction>(request);
        var articlesToUpdate = new List<Domain.Entities.Article>();

        foreach (var requestArticle in request.Articles)
        {
            var articleEntity = await _articleRepository.Get(requestArticle.Id, cancellationToken);
            if (articleEntity != null)
            {
                articleEntity.Quantity = articleEntity.Quantity - requestArticle.Quantity;
                articleEntity.LastUpdatedDate = DateTime.Now;

                articlesToUpdate.Add(articleEntity);
            }
            else
            {

                return new StatusCodeResponse() { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        var paymentsToAdd = _mapper.Map<List<Domain.Entities.Payment>>(request.Payments);

        await _transactionRepository.AddTransaction(entity, articlesToUpdate, paymentsToAdd);

        return new StatusCodeResponse() { StatusCode = HttpStatusCode.OK };

    }
}
