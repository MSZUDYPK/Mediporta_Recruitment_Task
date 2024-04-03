using System.Net;
using Mediporta.Core.Shared;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mediporta.Infrastructure.Exceptions;

public class DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error occurred");
        
        var statusCode = exception switch
        {
            CustomException  => httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest,
            _ => httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError
        };

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Type = exception.GetType().Name,
            Title = "An unexpected error occurred while processing your request.",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        }, cancellationToken: cancellationToken);

        return true;
    }
}