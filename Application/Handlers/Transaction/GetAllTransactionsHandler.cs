using Application.Command;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Handlers.Transaction
{
    public sealed class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsRequest, List<GetAllTransactionsResponse>>
    {
        private readonly ITransactionRepository _transactionsRepository;
        private readonly IMapper _mapper;

        public GetAllTransactionsHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            //_unitOfWork = unitOfWork;
            _transactionsRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllTransactionsResponse>> Handle(GetAllTransactionsRequest request, CancellationToken cancellationToken)
        {
           var articles= await _transactionsRepository.GetAll(cancellationToken);

            var articlesResponse =  _mapper.Map<List<GetAllTransactionsResponse>>(articles);

            return articlesResponse;
        }
    }

    public sealed record GetAllTransactionsRequest(): IRequest<List<GetAllTransactionsResponse>>;
    public sealed record GetAllTransactionsResponse() {
        public Guid Id { get; set; }
        [SwaggerSchema(Required = new[] { "Sum" })]
        public double Sum { get; set; }
        public string Bank { get; set; }
        public DateTime DueDate { get; set; }
        public bool DiscountApplied { get; set; }
        public bool SplitPayment { get; set; }

       // public List<ArticleRequest> Articles { get; set; }
        //public List<PaymentRequest> Payments { get; set; }

    }
}
