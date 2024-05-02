using Application.Command;
using Application.Command.Common;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Handlers.Payment;

public sealed class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, StatusCodeResponse>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public DeletePaymentHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }
    public async Task<StatusCodeResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.Get(request.id, cancellationToken);

        if (payment != null)
        {
            _paymentRepository.Delete(payment);

            return new StatusCodeResponse() { StatusCode = HttpStatusCode.OK };
        }

        return new StatusCodeResponse() { StatusCode = HttpStatusCode.NotFound };

    }
}
