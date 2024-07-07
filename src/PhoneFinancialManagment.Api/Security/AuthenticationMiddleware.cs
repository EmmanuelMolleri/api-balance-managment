using PhoneFinancialManagment.Domain.Services;

namespace PhoneFinancialManagment.Api.Security;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationMiddleware(RequestDelegate next, IAuthenticationService authenticationService)
    {
        _next = next;
        _authenticationService = authenticationService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;
        if (path.StartsWithSegments("/swagger") ||
            path.StartsWithSegments("/health") ||
            path.StartsWithSegments("/metrics"))
        {
            await _next(context);
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            var userClaims = _authenticationService.ValidateTokenAndGetUserClaims(token);

            context.Items["Token"] = token;

            if (userClaims != null)
            {
                context.Items["UserId"] = userClaims.UserId;
                context.Items["Roles"] = userClaims.Roles;
            }
        }

        if (!context.Items.ContainsKey("UserId"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }
}
