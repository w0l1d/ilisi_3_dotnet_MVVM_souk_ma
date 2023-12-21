using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AmazonShoping.Filters;

public class LoggedInUserFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Your custom authorization logic goes here
        // For example, check for a specific cookie, role, or any other condition

        bool isAuthorized = CheckAuthorizationLogic(context.HttpContext);

        if (!isAuthorized)
        {
            context.Result = new UnauthorizedResult(); // Or redirect to an unauthorized page
            Console.Write("hello you are not authorized");
        }

        base.OnActionExecuting(context);
    }

    private bool CheckAuthorizationLogic(HttpContext context)
    {
        // Your custom authorization logic goes here
        // For example, check for a specific cookie, role, or any other condition

        // For demonstration purposes, let's check if a specific cookie is present
        var authCookie = context.Request.Cookies["AuthorizationCookie"];
        return !string.IsNullOrEmpty(authCookie);
    }
}