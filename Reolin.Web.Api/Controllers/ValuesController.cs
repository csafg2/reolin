using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Reolin.Web.Api.Controllers
{
    public class MyViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    
    public class SampleController : Controller
    {
        public IActionResult Index()
        {
            return Json(new { Message = "123" });
        }
    }



    public class ValuesController : BaseController
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger, IServiceProvider provider)
        {
            this._logger = logger;
        }


        public string Get2()
        {

            return "HELLOW WORLD!!";
        }

        [Authorize]
        public string Get()
        {

            return "HELLOW WORLD!!";
        }
    }

}

