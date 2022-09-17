using System.Net;
using System.Security.Claims;
using System.Text.Json;
using FluentValidation;
using HomeBrewery.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewery.WebApi.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case DbUpdateException dbUpdateException:
                code = HttpStatusCode.Conflict;
                result = JsonSerializer.Serialize(new
                {
                    err = dbUpdateException.InnerException?.Message
                });
                break;
            case AccessForbiddenException:
                code = HttpStatusCode.Forbidden;
                break;
            case UnsupportedMediaTypeException:
                code = HttpStatusCode.UnsupportedMediaType;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { errpr = exception.Message });
        }

        var userId = !context.User.Identity!.IsAuthenticated
            ? "Not authenticated."
            : context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        _logger.LogError(exception, userId, result);

        await context.Response.WriteAsync(result);
    }
}