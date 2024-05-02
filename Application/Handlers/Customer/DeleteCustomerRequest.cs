using Application.Command;
using Application.Command.Common;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Handlers.Customer;

public sealed class DeleteCustomerRequest : IRequestHandler<DeleteCustomerCommand, StatusCodeResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DeleteCustomerRequest(ICustomerRepository custumerRepository, IMapper mapper)
    {
        _customerRepository = custumerRepository;
        _mapper = mapper;
    }
    public async Task<StatusCodeResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.Get(request.id, cancellationToken);

        if (entity != null)
        {
            _customerRepository.Delete(entity);

            return new StatusCodeResponse() { StatusCode = HttpStatusCode.OK };
        }

        return new StatusCodeResponse() { StatusCode = HttpStatusCode.NotFound };

    }
}
