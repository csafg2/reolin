using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Reolin.Web.Api.Infra.mvc
{
    public class BaseController : Controller
    {
        protected IActionResult Error(Exception ex)
        {
            return this.Error(ex.Message);
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