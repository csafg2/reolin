using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.mvc;
using System;
using Microsoft.AspNetCore.Authorization;

#pragma warning disable CS1591
namespace Reolin.Web.Api.Controllers
{
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
        public string Get()
        {

            return "HELLOW WORLD!!";
        }
        
    }
}

#pragma warning restore CS1591