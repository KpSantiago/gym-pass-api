using System.Net;
using GymPass.API.HttpResponses;
using GymPass.Shared.Exceptions;
using GymPass.Shared.Exceptions.ModuleExtensions;
using Newtonsoft.Json;

namespace GymPass.API.Middlewares;

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            if (ex is not RootException)
            {
                _logger.LogError($"Something went wrong: {ex}");
                throw new Exception(ex.Message);
            }
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.GetErrorStatusCode();

        var responseError = new ResponseError()
        {
            status = context.Response.StatusCode,
            error = exception is RootException ? exception.GetErrorType() : "Internal Server Error",
            message = exception.Message,
            timestamp = DateTime.UtcNow
        };

        return context.Response.WriteAsJsonAsync(responseError);
    }
}