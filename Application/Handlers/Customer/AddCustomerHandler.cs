using Application.Command;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Handlers.Customer;

public sealed class AddCustomerHandler : IRequestHandler<AddCustomerCommand>
{
    private readonly ICustomerRepository _CustomerRepository;
    private readonly IMapper _mapper;
    public AddCustomerHandler(ICustomerRepository CustomerRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _CustomerRepository = CustomerRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Customer>(request);

        _CustomerRepository.Add(entity);

        return;
    }
}
