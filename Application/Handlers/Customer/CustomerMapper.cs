using Application.Command;
using Application.Command.Common;
using AutoMapper;

namespace Application.Handlers.Customer;

public sealed class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<GetCustomerRequest, Domain.Entities.Customer>();
        CreateMap<AddCustomerCommand, Domain.Entities.Customer>();
        CreateMap<Domain.Entities.Customer, GetCustomerResponse>();
        CreateMap<UpdateCustomerRequest, Domain.Entities.Customer>();
        CreateMap<Domain.Entities.Customer, StatusCodeResponse>();
    }
}
