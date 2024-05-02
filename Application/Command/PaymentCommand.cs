using Application.Command.Common;
using Domain.Enums;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Command;

public record AddPaymentCommand() : IRequest
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [SwaggerSchema(ReadOnly = true)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [SwaggerSchema(ReadOnly = true)]
    public DateTime? LastUpdatedDate { get; set; } = DateTime.MinValue;
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime PaymentDate { get; set; }
    public Guid ProccessedBy { get; set; }
};

public record DeletePaymentCommand(Guid id) : IRequest<StatusCodeResponse>;
