using fixmycity.security;

namespace fixmycity.MiddleWares;

public class UserContextMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, CurrentUser currentUser)
    {
        var userId = context.Request.Headers["X-User-Id"].ToString();
        var role = context.Request.Headers["X-User-Role"].ToString();

        if (string.IsNullOrEmpty(userId))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing X-User-Id");
            return;
        }

        currentUser.Id = userId;
        currentUser.Role = role;

        await next(context);
    }
}