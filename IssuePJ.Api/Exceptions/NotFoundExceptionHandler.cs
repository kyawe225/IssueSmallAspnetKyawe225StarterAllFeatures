using System;
using IssuePJ.Api.Resources;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;

namespace IssuePJ.Api.Exceptions;

public class NotFoundExceptionHandler : IExceptionHandler
{
    private readonly ILogger<NotFoundExceptionHandler> _logger;
    private readonly IStringLocalizer<NotFoundResource> _localizer;
    public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger, IStringLocalizer<NotFoundResource> localizer)
    {
        _logger = logger;
        _localizer = localizer;
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
                new { error = _localizer["Not Found"].Value.Replace("{{item}}", ((NotFoundException)exception).Type) },
                cancellationToken);

            return true; // Indicates the exception has been handled
        }
        return false;
    }
}
