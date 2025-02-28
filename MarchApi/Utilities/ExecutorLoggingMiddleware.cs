using System.Security.Claims;

namespace MarchApi.Utilities;

/// <summary> Middleware for getting user from jwt token and log it </summary>
public class ExecutorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ExecutorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Extract user info from the JWT token or set to "Anonymous" if not available
        string executor = "Anonymous"; // Default to Anonymous
        string? userId = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;
        if (userId is not null)
        {
            executor = userId;  // Use the user ID from the JWT token
        }

        // Push user info to the log context
        Serilog.Context.LogContext.PushProperty("Executor", executor);  // Adds the 'Executor' property to the global log context

        // Call the next middleware in the pipeline
        await _next(context);
    }
}
