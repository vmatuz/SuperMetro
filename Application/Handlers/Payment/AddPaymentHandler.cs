using Application.Command;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Handlers.Payment;

public sealed class AddPaymentHandler : IRequestHandler<AddPaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    public AddPaymentHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
       var entity = _mapper.Map<Domain.Entities.Payment> (request);

        _paymentRepository.Add(entity);

        return;
    }
}
