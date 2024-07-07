using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PhoneFinancialManagment.Infraestructure.Security;

public class AuthorizeRolesAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public AuthorizeRolesAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roles = context.HttpContext.Items["Roles"] as List<string>;

        if (roles == null || !_roles.Any(role => roles.Contains(role)))
        {
            context.Result = new ForbidResult();
        }
    }
}