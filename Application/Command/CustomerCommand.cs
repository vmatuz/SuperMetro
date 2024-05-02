using Application.Command.Common;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Command;

public record AddCustomerCommand() : IRequest {  

    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [SwaggerSchema(ReadOnly = true)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastUpdatedDate { get; set; }
    public string Name { get; set; }
    public string CreditCard { get; set; }
    [SwaggerSchema(Required = new[] { "Address" })]
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    [SwaggerSchema(Required = new[] { "Email" })]
    [EmailAddress]
    public string Email { get; set; }
    public string FIN { get; set; }
    public bool IsArchived { get; set; }
    public bool CanSendPromotionsEmails { get; set; }
};
public record DeleteCustomerCommand(Guid id) : IRequest<StatusCodeResponse>;