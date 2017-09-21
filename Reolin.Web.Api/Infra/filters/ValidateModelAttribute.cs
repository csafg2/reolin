#pragma warning disable CS1591
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Reolin.Data;
using System;
using System.Threading.Tasks;
using System.Reflection;
using Reolin.Web.Security.Jwt;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

namespace Reolin.Web.Api.Infra.Filters
{
    public class CheckPermissionAttribute : Attribute, IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return Task.FromResult(0);
            //DataContext dbContext = (DataContext)context.HttpContext.RequestServices.GetService(typeof(DataContext));
            //var model = context.ActionArguments["model"];
            //if (model == null)
            //{
            //    return;
            //}

            //PropertyInfo info = model.GetType().GetProperty("ProfileId");

            //if (info == null)
            //{
            //    return;
            //}

            //int profileId = (int)info.GetValue(model);
            
            //int userId = int.Parse(context.HttpContext.User.Claims.Where(c => c.Type == JwtConstantsLookup.ID_CLAIM_TYPE).FirstOrDefault().Value);

            //var ids = await dbContext.Profiles.Where(p => p.UserId == userId).Select(p => p.Id).ToArrayAsync();

            //if (!ids.Any(i => i == profileId))
            //{
            //    context.Result = new UnauthorizedResult();
            //}
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
