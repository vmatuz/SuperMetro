using Application.Repositories;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Handlers.Payment;

public sealed class UpdatePaymentHandler : IRequestHandler<UpdatePaymentRequest, UpdatePaymentResponse>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public UpdatePaymentHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<UpdatePaymentResponse> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
    {
        var paymentEntity = await _paymentRepository.Get(request.Id, cancellationToken);

        if (paymentEntity != null)
        {
            paymentEntity.Amount = request.amount;
            _paymentRepository.Update(paymentEntity);
            return new UpdatePaymentResponse() { StatusCode = HttpStatusCode.OK };
        }
        else
        {
            return new UpdatePaymentResponse() { StatusCode = HttpStatusCode.NotFound };
        }
    }
}

public sealed record UpdatePaymentRequest(Guid Id, double amount) : IRequest<UpdatePaymentResponse>;

public sealed record UpdatePaymentResponse
{
    public HttpStatusCode StatusCode { get; set; }
}
