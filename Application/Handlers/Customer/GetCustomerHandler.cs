using MediatR;
using AutoMapper;
using Application.Repositories;

namespace Application.Handlers.Customer;

public sealed class GetCustomerHandler : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        //_unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var entity =   _customerRepository.GetCustomerByEmail(request.Email);

        // await _unitOfWork.Save(cancellationToken);

        return  _mapper.Map<GetCustomerResponse>(entity);
    }
}

public sealed record GetCustomerRequest(string Email) : IRequest<GetCustomerResponse>;

public sealed record GetCustomerResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public string Name { get; set; }
    public string CreditCard { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string FIN { get; set; }
    public bool IsArchived { get; set; }
    public bool CanSendPromotionsEmails { get; set; }
}
