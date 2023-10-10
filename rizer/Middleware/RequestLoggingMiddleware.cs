using System.Diagnostics;

namespace rizer.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
            
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("{wtf} HTTP-{method} completed status {status} in {duration} ms",
                context.Request.Path, context.Request.Method, context.Response.StatusCode, stopwatch.Elapsed.Milliseconds);
        }
    }
}