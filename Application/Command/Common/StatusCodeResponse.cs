using Microsoft.AspNetCore.Http;
using System.Net;

namespace Application.Command.Common;

public sealed record StatusCodeResponse
{
    public HttpStatusCode StatusCode { get; set; }
}
