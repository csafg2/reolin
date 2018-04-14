using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class HttpsController : Controller
    {
        [HttpGet]
        [Route(".well-known/acme-challenge/M83pb5BDlmKUW5PwP_2dxx0riS9bHoB-gnsAtzUbROY")]
        public ActionResult Get()
        {
            this.HttpContext.Response.ContentType = "text/plain";
            return Content("M83pb5BDlmKUW5PwP_2dxx0riS9bHoB-gnsAtzUbROY.zzZ2blpp57ntAGjuctXa2jahtLV04hVxn_eTfcEg-fU");
        }
    }
}
