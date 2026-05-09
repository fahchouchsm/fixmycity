using fixmycity.Enums;
using fixmycity.security;

namespace fixmycity.MiddleWares;

public class UserContextMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, CurrentUser currentUser)
    {
        var userId = context.Request.Headers[GatewayHeader.Id].ToString();
        var role = context.Request.Headers[GatewayHeader.Role].ToString();

        if (string.IsNullOrEmpty(userId))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing X-User-Id");
            return;
        }

        currentUser.Id = userId;
        currentUser.Role = role;
        currentUser.email = context.Request.Headers[GatewayHeader.Email].ToString();
        currentUser.name = context.Request.Headers[GatewayHeader.Name].ToString();
        currentUser.lastName = context.Request.Headers[GatewayHeader.LastName].ToString();

        await next(context);
    }
}