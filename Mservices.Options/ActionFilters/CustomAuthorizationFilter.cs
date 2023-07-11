using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mservices.Options.ActionFilters;

public class CustomAuthorizationFilter : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            // Redirect to login page or return unauthorized response
            context.Result = new RedirectToActionResult("Login", "Test", null);
        }
        
        return Task.CompletedTask;
    }
}