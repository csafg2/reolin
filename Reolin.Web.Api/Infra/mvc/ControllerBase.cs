using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Linq;
using Reolin.Web.Security.Jwt;

namespace Reolin.Web.Api.Infra.mvc
{
    public class BaseController : Controller
    {
        
        protected int UserId
        {
            get
            {
                return int.Parse(User.Claims.Where(c => c.Type == JwtConstantsLookup.ID_CLAIM_TYPE)
                    .FirstOrDefault().Value);
            }
        }

        protected IActionResult Error(Exception ex)
        {
            return this.Error(ex.Message);
        }

        protected IActionResult Error(HttpStatusCode code, string message)
        {
            // wrap message into an object in order to be properly json serializable
            return this.ApiResult(code, new { Message = message });
        }

        protected IActionResult Error(string message)
        {
            return this.ApiResult(HttpStatusCode.InternalServerError, new { Message = message });
        }

        protected IActionResult ApiResult(HttpStatusCode code, object data)
        {
            return new ApiResult(code, data);
        }
    }
}