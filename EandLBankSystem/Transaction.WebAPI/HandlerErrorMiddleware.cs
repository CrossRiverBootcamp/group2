using System.Net;


namespace Transaction.WebAPI;

public class HandlerErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HandlerErrorMiddleware> _logger;
    public HandlerErrorMiddleware(RequestDelegate next, ILogger<HandlerErrorMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

public async Task Invoke(HttpContext httpContext)
{
    try
    {
        await _next(httpContext);
    }
    catch (Exception ex)
    {
        var response = httpContext.Response;
        response.ContentType = "application/json";
        HttpStatusCode statusCode = ex switch
        {
            ArgumentNullException or ArgumentException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Forbidden,
            KeyNotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError,
        };
        response.StatusCode = (int)statusCode;
        await response.WriteAsync(ex.Message);

        _logger.Log(LogLevel.Error, ex.Message, response.StatusCode ,ex.StackTrace);
    }
}


public static class HandleErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseHandleErrorMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HandlerErrorMiddleware>();
    }
}