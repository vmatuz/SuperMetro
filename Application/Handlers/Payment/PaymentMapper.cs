using Application.Command;
using AutoMapper;

namespace Application.Handlers.Payment;

public sealed class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<GetPaymentRequest, Domain.Entities.Payment>();
        CreateMap<AddPaymentCommand, Domain.Entities.Payment>();
        CreateMap<Domain.Entities.Payment, GetPaymentResponse>();
        CreateMap<UpdatePaymentRequest, Domain.Entities.Payment>();
        CreateMap<Domain.Entities.Payment, UpdatePaymentResponse>();
        CreateMap<PaymentRequest, Domain.Entities.Payment> ();
    }
}
