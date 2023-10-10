using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using rizer.Middleware;

namespace rizer_tests.MIddleware;

public class RequestLoggingMiddlewareTest
{
    [Test]
    public void Create()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var middleware = new RequestLoggingMiddleware(MockInvoke, loggerFactory);
        var httpContext = new DefaultHttpContext();
        var task = middleware.InvokeAsync(httpContext);
        task.Wait();
    }

    private static Task<int> MockInvoke(HttpContext aContext)
    {
        return Task.FromResult(10);
    }
}