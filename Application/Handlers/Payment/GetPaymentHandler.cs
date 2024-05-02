using MediatR;
using AutoMapper;
using Application.Repositories;
using Domain.Enums;

namespace Application.Handlers.Payment;

public sealed class GetPaymentHandler : IRequestHandler<GetPaymentRequest, GetPaymentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<GetPaymentResponse> Handle(GetPaymentRequest request, CancellationToken cancellationToken)
    {
        var paymentEntity = await _paymentRepository.Get(request.Id,cancellationToken);

        // await _unitOfWork.Save(cancellationToken);

        return _mapper.Map<GetPaymentResponse>(paymentEntity);
    }
}

public sealed record GetPaymentRequest(Guid Id) : IRequest<GetPaymentResponse>;

public sealed record GetPaymentResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime PaymentDate { get; set; }
    public Guid ProccessedBy { get; set; }
}
