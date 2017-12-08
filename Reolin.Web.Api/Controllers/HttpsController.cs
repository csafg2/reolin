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
        [Route(".well-known/acme-challenge/wAic3ky8tc5g6zLPt96_FjLXpKPKwFP6VG4zIqymG_U")]
        public ActionResult Get()
        {
            this.HttpContext.Response.ContentType = "text/plain";
            return Content("wAic3ky8tc5g6zLPt96_FjLXpKPKwFP6VG4zIqymG_U.zzZ2blpp57ntAGjuctXa2jahtLV04hVxn_eTfcEg-fU");
        }
    }
}
