using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.CustomExceptions;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        int statusCode;
        string errorMessage;

        if (exception is RecipeNotFoundException)
        {
            statusCode = (int)HttpStatusCode.NotFound;
            errorMessage = exception.Message;
        }
        else if (exception is EmptyImageException)
        {
            statusCode = (int)HttpStatusCode.NotFound;
            errorMessage = exception.Message;
        }
        else if (exception is InvalidImageTypeException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            errorMessage = exception.Message;
        }
        else
        {
            _logger.LogError(exception, exception.Message);
            statusCode = (int)HttpStatusCode.InternalServerError;
            errorMessage = "An error occurred while processing your request.";
        }

        var errorResponse = new
        {
            Status = statusCode,
            ExceptionMessage = errorMessage,
        };

        // Serialize the error response to JSON
        var jsonResponse = JsonSerializer.Serialize(errorResponse);

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(jsonResponse);
    }
}
