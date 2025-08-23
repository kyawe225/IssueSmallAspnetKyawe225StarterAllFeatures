using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IssuePJ.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
            _logger.LogError(
                $"An unhandled exception occurred: {exception.Message}",
                exception);

            // You can customize the response based on the exception type
            // For example, return different status codes or error messages
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(
                new { error = $"Internal Server Error Cause. Please contact Developer!" },
                cancellationToken);

            return true; // Indicates the exception has been handled
    }
}
