using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Api.GlobalHandling;

public class TimeOutExceptionHandler : IExceptionHandler
{


    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if (exception is TimeoutException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)HttpStatusCode.RequestTimeout,
                Type = exception.GetType().Name,
                Title = "A timeout occurred",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            });
            return true;
        }
        return false;
    }
}