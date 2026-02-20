using Microsoft.AspNetCore.Http;
using MiniWebService.Services;

namespace MiniWebService.Middlewares;

/// <summary>
/// Обеспечивает наличие идентификатора запроса.
/// </summary>
public sealed class RequestIdMiddleware
{
    private readonly RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next) => _next = next;

    public Task Invoke(HttpContext context)
    {
        _ = RequestId.GetOrCreate(context);
        return _next(context);
    }
}
