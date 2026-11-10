using System.Net;

namespace Catalog.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate _next, ILogger<ExceptionHandlingMiddleware> _logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            if (context.Response.HasStarted)
            {
                throw;
            }

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            KeyNotFoundException => (
                HttpStatusCode.NotFound,
                "Resource not found."),
            InvalidOperationException => (
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred."),
            _ =>
                (HttpStatusCode.InternalServerError,
                 "An unexpected error occurred.")
        };

        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = (int)statusCode,
            error = message
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
