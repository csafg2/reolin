using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Linq;

namespace Reolin.Web.Api.Infra.mvc
{
    public class BaseController : Controller
    {
        protected string Username
        {
            get
            {
                return User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault().Value;
            }
        }

        const string ID_CLAIM_TYPE = "Id";
        protected int UserId
        {
            get
            {
                return int.Parse(User.Claims.Where(c => c.Type == ID_CLAIM_TYPE).FirstOrDefault().Value);
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