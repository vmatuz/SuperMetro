using Application.Repositories;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Command.Common;

public sealed class UpdateCustomerHandler : IRequestHandler<UpdateCustomerRequest, StatusCodeResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<StatusCodeResponse> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.Get(request.Id,cancellationToken);

        if (entity != null)
        {
            entity.Address = request.address;
            _customerRepository.Update(entity);

            return new StatusCodeResponse() { StatusCode = HttpStatusCode.OK };
        }
        else {
            return new StatusCodeResponse() { StatusCode = HttpStatusCode.NotFound };
        }
    }
}

public sealed record UpdateCustomerRequest(Guid Id, string address) : IRequest<StatusCodeResponse>;
