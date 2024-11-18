using System.Net;
using GymPass.API.HttpResponses;
using GymPass.Shared.Exceptions;
using GymPass.Shared.Exceptions.ModuleExtensions;
using Newtonsoft.Json;

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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (exception is RootException)
        {
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseError()
            {
                status = exception.GetErrorStatusCode(),
                error = exception.GetErrorType(),
                message = exception.Message,
                timestamp = DateTime.Now
            }));
        }

        return context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseError()
        {
            status = 500,
            error = "Internal Server Error",
            message = exception.Message,
            timestamp = DateTime.Now
        }));
    }
}