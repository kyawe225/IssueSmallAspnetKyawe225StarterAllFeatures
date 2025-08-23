using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IssuePJ.Api.Exceptions;

public class NotFoundExceptionHandler : IExceptionHandler
{
    private readonly ILogger<NotFoundExceptionHandler> _logger;
    public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        if (exception is NotFoundException)
        {
            _logger.LogError(
                $"An unhandled exception occurred: {exception.Message}",
                exception);

            // You can customize the response based on the exception type
            // For example, return different status codes or error messages
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(
                new { error = $"{((NotFoundException)exception).Type} Not Found" },
                cancellationToken);

            return true; // Indicates the exception has been handled
        }
        return false;
    }
}
