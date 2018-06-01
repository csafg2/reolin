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
        [Route(".well-known/acme-challenge/M_5hX9b9BeoNGipbWmgRThRq0aIkxZX_PeZ4ogdqIgo")]
        public ActionResult Get()
        {
            this.HttpContext.Response.ContentType = "text/plain";
            return Content("M_5hX9b9BeoNGipbWmgRThRq0aIkxZX_PeZ4ogdqIgo.Np7e89O9cXvnW3hP2YV0PtTjEPODvI3Fs8LUwqzh77c");
        }
    }
}
