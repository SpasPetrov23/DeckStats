using System.Net;
using DeckStats.Common.Errors;

namespace DeckStats.API.Utils.ExceptionHandling;

public class ExceptionHandlerMiddleware
{
    private static string INTERNAL_SERVER_ERROR = nameof(INTERNAL_SERVER_ERROR);
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly string _responseContentType = "application/json";

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
        catch (AppException e)
        {
            _logger.Log(LogLevel.Error, e, e.Message);
            
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = _responseContentType;
            ErrorDetails errorResponse = new(context.Response.StatusCode, e.Message);
            if (e.Args.Count > 0)
            {
                errorResponse = new(context.Response.StatusCode, e.Message, e.Args);
            }

            await context.Response.WriteAsync(errorResponse.ToJson());
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e, e.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = _responseContentType;
            await context.Response.WriteAsync(new ErrorDetails(context.Response.StatusCode, INTERNAL_SERVER_ERROR).ToJson());
        }
    }
}
