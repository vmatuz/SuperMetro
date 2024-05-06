using Application.Command.Common;
using Domain.Enums;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Command;

public sealed record AddTransactionCommand() : IRequest<StatusCodeResponse>
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [SwaggerSchema(Required = new[] { "Sum" })]
    public double Sum { get; set; }
    public string Bank { get; set; }
    public DateTime DueDate { get; set; }
    public bool DiscountApplied { get; set; }
    public bool SplitPayment { get; set; }

    [SwaggerSchema(Required = new[] { "Articles" })]
    public List<ArticleRequest> Articles { get; set; }
    [SwaggerSchema(Required = new[] { "Payments" })]
    public List<PaymentRequest> Payments { get; set; }
}

public class ArticleRequest
{
    public Guid Id { get; set; }

    [SwaggerSchema(Required = new[] { "Quantity" })]
    public int Quantity { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public DateTime? LastUpdatedDate { get; set; } = DateTime.UtcNow;
}
public class PaymentRequest
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [SwaggerSchema(Required = new[] { "Amount" })]
    public double Amount { get; set; }
    [SwaggerSchema(Required = new[] { "PaymentType" })]
    public PaymentType PaymentType { get; set; }
    [SwaggerSchema(Required = new[] { "CustomerId" })]
    public Guid CustomerId { get; set; }
    [SwaggerSchema(Required = new[] { "PaymentDate" })]
    public DateTime PaymentDate { get; set; }
}
