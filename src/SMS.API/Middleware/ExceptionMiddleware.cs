using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Exceptions.BaseExceptions;
using System.Net;
using System.Text.Json;

namespace SMS.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            context.Response.ContentType = "application/json";

            _logger.LogError($"An error occurred. Message: ${ex.Message} \n Details: {ex.StackTrace}");

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(NotFoundException))
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            else if (exceptionType == typeof(BadRequestException))
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            else
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = ex.Message,
                Detail = ex.StackTrace
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedError = JsonSerializer.Serialize(error, jsonOptions);

            await context.Response.WriteAsync(serializedError);
        }
    }
}
