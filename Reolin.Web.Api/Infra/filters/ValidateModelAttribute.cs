using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Reolin.Web.Api.Infra.filters
{
    public class CompareRequestInputsAttribute : Attribute, IAction
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.User.Claims.GetUsernameClaim
        }
    }

    public class RequireValidModelAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
