namespace MarchApi.Utilities;

/// <summary> Middleware log nếu api response StatusCode 401 hoặc 403 </summary>
public class AuthorizationLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            Log.Warning("Unauthorized request to {Path}", context.Request.Path);
        }

        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            Log.Warning("Forbidden request to {Path} by userId {User}",
                context.Request.Path, context.GetUserIdFromJwt());
        }
    }
}
