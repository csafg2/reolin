#pragma warning disable CS1591

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    public class ValuesController : BaseController
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger, IServiceProvider provider)
        {
            this._logger = logger;
        }

        [HttpGet]
        public string Get2()
        {

            return "HELLOW WORLD!!";
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/[action]")]
        public string Get()
        {

            return "HELLOW WORLD!!";
        }
        
    }
}