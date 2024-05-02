using Application.Command.Common;
using Domain.Enums;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Command
{
    public record AddArticleCommand() : IRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [SwaggerSchema(ReadOnly = true)]

        public DateTime? LastUpdatedDate { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool AllowDiscount { get; set; }
        public int BatchNumber { get; set; }
        public ArticleTypes Type { get; set; }

    };
    public record DeleteArticleCommand(Guid id) : IRequest<StatusCodeResponse>;
}
